using Stambat.Domain.Enums;

namespace Stambat.Domain.ValueObjects;

public sealed record WalletPassRequest(
    Guid WalletPassId,
    string ClassId,
    Guid TenantId,
    string TenantName,
    string CardTitle,
    string? CardDescription,
    decimal RequiredBalance,
    decimal CurrentBalance,
    RedemptionType RedemptionType,
    string? RewardDescription,
    string QrCodeContent,
    string? LogoUrl,
    string? PrimaryColor,
    string? SecondaryColor,
    string? TermsAndConditions,
    DateTime? ExpiresAt);
