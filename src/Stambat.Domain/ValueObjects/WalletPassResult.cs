namespace Stambat.Domain.ValueObjects;

public sealed record WalletPassResult(
    string? ApplePassSerialNumber,
    string? GooglePayId,
    byte[]? PassFileBytes,
    string? GoogleSaveUrl,
    string? ApplePassUrl);
