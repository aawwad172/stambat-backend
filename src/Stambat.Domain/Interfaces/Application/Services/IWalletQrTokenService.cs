namespace Stambat.Domain.Interfaces.Application.Services;

public interface IWalletQrTokenService
{
    string GenerateQrToken(Guid walletPassId, Guid tenantId);
    (Guid WalletPassId, Guid TenantId) DecodeQrToken(string encryptedToken);
}
