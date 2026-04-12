using MediatR;

namespace Stambat.Application.CQRS.Commands.Scanning;

public sealed record ScanStampCommand(
    string QrToken,
    int StampsToAdd = 1,
    string? Note = null) : IRequest<ScanStampCommandResult>;

public sealed record ScanStampCommandResult(
    Guid WalletPassId,
    int NewStampCount,
    int StampsRequired,
    bool IsCompleted,
    string CardTitle);
