using Stambat.Domain.Enums;

namespace Stambat.Domain.ValueObjects;

public sealed record WalletPassUpdateRequest(
    Guid WalletPassId,
    string? ApplePassSerialNumber,
    string? GooglePayId,
    int CurrentStamps,
    int StampsRequired,
    WalletPassStatus Status,
    string? QrCodeContent,
    string? LogoUrl,
    string? PrimaryColor,
    string? SecondaryColor);
