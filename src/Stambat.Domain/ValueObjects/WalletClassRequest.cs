namespace Stambat.Domain.ValueObjects;

public sealed record WalletClassRequest(
    Guid CardTemplateId,
    Guid TenantId,
    string TenantName,
    string CardTitle,
    string? CardDescription,
    int StampsRequired,
    string? RewardDescription,
    string? LogoUrl,
    string? PrimaryColor,
    string? SecondaryColor);
