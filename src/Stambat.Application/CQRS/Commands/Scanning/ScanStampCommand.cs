using MediatR;

using Stambat.Domain.Enums;

namespace Stambat.Application.CQRS.Commands.Scanning;

public sealed record ScanStampCommand(
    string QrToken,
    decimal AmountToAdd = 1,
    string? Note = null) : IRequest<ScanStampCommandResult>;

public sealed record ScanStampCommandResult(
    Guid WalletPassId,
    decimal CurrentBalance,
    decimal RequiredBalance,
    RedemptionType RedemptionType,
    bool IsCompleted,
    string CardTitle);
