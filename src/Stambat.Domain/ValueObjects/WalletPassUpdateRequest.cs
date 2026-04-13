using Stambat.Domain.Enums;

namespace Stambat.Domain.ValueObjects;

public sealed record WalletPassUpdateRequest(
    Guid WalletPassId,
    string? ApplePassSerialNumber,
    string? GooglePayId,
    decimal CurrentBalance,
    decimal RequiredBalance,
    RedemptionType RedemptionType,
    WalletPassStatus Status,
    string? QrCodeContent,
    string? LogoUrl,
    string? PrimaryColor,
    string? SecondaryColor,
    DateTime? ExpiresAt);
