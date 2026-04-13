using Stambat.Domain.Enums;

namespace Stambat.Domain.ValueObjects;

public sealed record WalletClassRequest(
    Guid CardTemplateId,
    Guid TenantId,
    string TenantName,
    string CardTitle,
    string? CardDescription,
    decimal RequiredBalance,
    RedemptionType RedemptionType,
    string? RewardDescription,
    string? LogoUrl,
    string? PrimaryColor,
    string? SecondaryColor);
