using System.Text.Json;

using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;

namespace Stambat.Application.Services;

public class WalletQrTokenService(ISecurityService securityService) : IWalletQrTokenService
{
    private readonly ISecurityService _securityService = securityService;

    public string GenerateQrToken(Guid walletPassId, Guid tenantId)
    {
        QrTokenPayload payload = new QrTokenPayload(
            walletPassId.ToString(),
            tenantId.ToString(),
            DateTimeOffset.UtcNow.ToUnixTimeSeconds());

        string json = JsonSerializer.Serialize(payload);
        return _securityService.EncryptString(json);
    }

    public (Guid WalletPassId, Guid TenantId) DecodeQrToken(string encryptedToken)
    {
        try
        {
            string json = _securityService.DecryptString(encryptedToken);
            QrTokenPayload? payload = JsonSerializer.Deserialize<QrTokenPayload>(json);

            if (payload is null
                || !Guid.TryParse(payload.WpId, out Guid walletPassId)
                || !Guid.TryParse(payload.Tid, out Guid tenantId))
            {
                throw new InvalidTokenException("The scanned QR code is invalid or corrupted.");
            }

            return (walletPassId, tenantId);
        }
        catch (InvalidTokenException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidTokenException("The scanned QR code is invalid or could not be decrypted.", ex);
        }
    }

    private sealed record QrTokenPayload(string WpId, string Tid, long Iat);
}
