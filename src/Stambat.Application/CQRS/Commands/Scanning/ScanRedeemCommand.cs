using MediatR;

namespace Stambat.Application.CQRS.Commands.Scanning;

public sealed record ScanRedeemCommand(
    string QrToken) : IRequest<ScanRedeemCommandResult>;

public sealed record ScanRedeemCommandResult(
    Guid WalletPassId,
    string CardTitle,
    string? RewardDescription,
    DateTime RedeemedAt);
