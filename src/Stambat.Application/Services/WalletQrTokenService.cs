using System.Text.Json;

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
        string json = _securityService.DecryptString(encryptedToken);
        var payload = JsonSerializer.Deserialize<QrTokenPayload>(json)
            ?? throw new InvalidOperationException("Invalid QR token payload.");

        return (Guid.Parse(payload.WpId), Guid.Parse(payload.Tid));
    }

    private sealed record QrTokenPayload(string WpId, string Tid, long Iat);
}
