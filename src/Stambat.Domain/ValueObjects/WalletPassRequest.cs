namespace Stambat.Domain.ValueObjects;

public sealed record WalletPassRequest(
    Guid WalletPassId,
    string ClassId,
    Guid TenantId,
    string TenantName,
    string CardTitle,
    string? CardDescription,
    int StampsRequired,
    int CurrentStamps,
    string? RewardDescription,
    string QrCodeContent,
    string? LogoUrl,
    string? PrimaryColor,
    string? SecondaryColor,
    string? TermsAndConditions);
