using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Walletobjects.v1;
using Google.Apis.Walletobjects.v1.Data;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Stambat.Domain.Enums;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Infrastructure.IClients;
using Stambat.Domain.ValueObjects;
using Stambat.Infrastructure.Clients.WalletPass.Options;

namespace Stambat.Infrastructure.Clients.WalletPass;

public class GoogleWalletPassProvider : IWalletPassProvider, IDisposable
{
    private readonly GoogleWalletOptions _options;
    private readonly ILogger<GoogleWalletPassProvider> _logger;
    private readonly WalletobjectsService _walletService;
    private readonly RSA _signingKey;
    private readonly string _clientEmail;

    private const string SaveUrlBase = "https://pay.google.com/gp/v/save";

    public WalletProviderType ProviderType => WalletProviderType.Google;

    public GoogleWalletPassProvider(
        IOptions<GoogleWalletOptions> options,
        ILogger<GoogleWalletPassProvider> logger)
    {
        _options = options.Value;
        _logger = logger;

        GoogleCredential credential = CredentialFactory
            .FromFile<ServiceAccountCredential>(_options.ServiceAccountKeyPath)
            .ToGoogleCredential()
            .CreateScoped(WalletobjectsService.Scope.WalletObjectIssuer);

        _walletService = new WalletobjectsService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = _options.ApplicationName
        });

        // Cache signing key and client email for JWT generation
        string serviceAccountJson = File.ReadAllText(_options.ServiceAccountKeyPath);
        JsonElement serviceAccount = JsonSerializer.Deserialize<JsonElement>(serviceAccountJson);

        _clientEmail = serviceAccount.GetProperty("client_email").GetString()!;
        _signingKey = RSA.Create();
        _signingKey.ImportFromPem(serviceAccount.GetProperty("private_key").GetString()!);
    }

    public async Task<WalletClassResult> CreateClassAsync(
        WalletClassRequest request,
        CancellationToken cancellationToken = default)
    {
        string classId = $"{_options.IssuerId}.{request.TenantId}_{request.CardTemplateId}";

        try
        {
            LoyaltyClass loyaltyClass = new()
            {
                Id = classId,
                IssuerName = request.TenantName,
                ProgramName = request.CardTitle,
                ReviewStatus = "UNDER_REVIEW",
                MultipleDevicesAndHoldersAllowedStatus = "ONE_USER_ALL_DEVICES",
                AccountNameLabel = "Member",
                AccountIdLabel = "Card ID"
            };

            if (!string.IsNullOrEmpty(request.LogoUrl))
            {
                loyaltyClass.ProgramLogo = new Image { SourceUri = new ImageUri { Uri = request.LogoUrl } };
            }

            if (!string.IsNullOrEmpty(request.PrimaryColor))
            {
                loyaltyClass.HexBackgroundColor = request.PrimaryColor;
            }

            LoyaltyClass result = await _walletService.Loyaltyclass
                .Insert(loyaltyClass)
                .ExecuteAsync(cancellationToken);

            _logger.LogInformation("Created Google Wallet loyalty class {ClassId}", result.Id);
            return new WalletClassResult(classId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create Google Wallet loyalty class {ClassId}", classId);
            throw new WalletProviderException($"Failed to create Google Wallet loyalty class: {ex.Message}", ex);
        }
    }

    public async Task UpdateClassAsync(
        WalletClassRequest request,
        string classId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            LoyaltyClass existing = await _walletService.Loyaltyclass
                .Get(classId)
                .ExecuteAsync(cancellationToken);

            existing.IssuerName = request.TenantName;
            existing.ProgramName = request.CardTitle;
            existing.ProgramLogo = string.IsNullOrEmpty(request.LogoUrl)
                ? null
                : new Image { SourceUri = new ImageUri { Uri = request.LogoUrl } };
            existing.HexBackgroundColor = request.PrimaryColor;
            existing.ReviewStatus = "UNDER_REVIEW"; // Must reset — Google rejects "APPROVED" on update

            await _walletService.Loyaltyclass
                .Update(existing, classId)
                .ExecuteAsync(cancellationToken);

            _logger.LogInformation("Updated Google Wallet loyalty class {ClassId}", classId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update Google Wallet loyalty class {ClassId}", classId);
            throw new WalletProviderException($"Failed to update Google Wallet loyalty class: {ex.Message}", ex);
        }
    }

    public async Task<WalletPassResult> CreatePassAsync(
        WalletPassRequest request,
        CancellationToken cancellationToken = default)
    {
        string objectId = $"{_options.IssuerId}.{request.WalletPassId}";

        try
        {
            LoyaltyObject loyaltyObject = BuildLoyaltyObject(objectId, request);

            await _walletService.Loyaltyobject
                .Insert(loyaltyObject)
                .ExecuteAsync(cancellationToken);

            _logger.LogInformation("Created Google Wallet loyalty object {ObjectId}", objectId);

            string saveUrl = GenerateSaveUrl(objectId, request.ClassId);

            return new WalletPassResult(
                ApplePassSerialNumber: null,
                GooglePayId: objectId,
                PassFileBytes: null,
                GoogleSaveUrl: saveUrl,
                ApplePassUrl: null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create Google Wallet loyalty object {ObjectId}", objectId);
            throw new WalletProviderException($"Failed to create Google Wallet loyalty object: {ex.Message}", ex);
        }
    }

    public async Task<WalletPassResult> UpdatePassAsync(
        WalletPassUpdateRequest request,
        CancellationToken cancellationToken = default)
    {
        string objectId = request.GooglePayId
            ?? $"{_options.IssuerId}.{request.WalletPassId}";

        try
        {
            LoyaltyObject patch = new()
            {
                LoyaltyPoints = new LoyaltyPoints
                {
                    Balance = new LoyaltyPointsBalance { Int__ = request.CurrentStamps },
                    Label = "Stamps"
                },
                State = request.Status == WalletPassStatus.Redeemed ? "COMPLETED" : null
            };

            await _walletService.Loyaltyobject
                .Patch(patch, objectId)
                .ExecuteAsync(cancellationToken);

            _logger.LogInformation("Updated Google Wallet loyalty object {ObjectId}", objectId);

            return new WalletPassResult(
                ApplePassSerialNumber: null,
                GooglePayId: objectId,
                PassFileBytes: null,
                GoogleSaveUrl: null,
                ApplePassUrl: null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update Google Wallet loyalty object {ObjectId}", objectId);
            throw new WalletProviderException($"Failed to update Google Wallet loyalty object: {ex.Message}", ex);
        }
    }

    private static LoyaltyObject BuildLoyaltyObject(string objectId, WalletPassRequest request)
    {
        LoyaltyObject loyaltyObject = new()
        {
            Id = objectId,
            ClassId = request.ClassId,
            State = "ACTIVE",
            LoyaltyPoints = new LoyaltyPoints
            {
                Balance = new LoyaltyPointsBalance { Int__ = request.CurrentStamps },
                Label = "Stamps"
            },
            Barcode = new Barcode
            {
                Type = "QR_CODE",
                Value = request.QrCodeContent,
                AlternateText = "Scan to stamp"
            },
            AccountId = request.WalletPassId.ToString(),
            AccountName = request.CardTitle
        };

        if (!string.IsNullOrEmpty(request.RewardDescription))
        {
            loyaltyObject.TextModulesData =
            [
                new TextModuleData
                {
                    Header = "Reward",
                    Body = request.RewardDescription
                }
            ];
        }

        return loyaltyObject;
    }

    private string GenerateSaveUrl(string objectId, string classId)
    {
        var payload = new
        {
            loyaltyObjects = new[]
            {
                new { id = objectId, classId }
            }
        };

        string jwt = CreateSignedJwt(payload);
        return $"{SaveUrlBase}/{jwt}";
    }

    private string CreateSignedJwt(object payload)
    {
        var claims = new Dictionary<string, object>
        {
            ["iss"] = _clientEmail,
            ["aud"] = "google",
            ["typ"] = "savetowallet",
            ["iat"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            ["payload"] = payload
        };

        string headerBase64 = Base64UrlEncoder.Encode(
            Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new { alg = "RS256", typ = "JWT" })));
        string claimsBase64 = Base64UrlEncoder.Encode(
            Encoding.UTF8.GetBytes(JsonSerializer.Serialize(claims)));

        string unsignedToken = $"{headerBase64}.{claimsBase64}";
        byte[] signature = _signingKey.SignData(
            Encoding.UTF8.GetBytes(unsignedToken),
            HashAlgorithmName.SHA256,
            RSASignaturePadding.Pkcs1);

        return $"{unsignedToken}.{Base64UrlEncoder.Encode(signature)}";
    }

    public void Dispose()
    {
        _signingKey.Dispose();
        _walletService.Dispose();
        GC.SuppressFinalize(this);
    }
}
