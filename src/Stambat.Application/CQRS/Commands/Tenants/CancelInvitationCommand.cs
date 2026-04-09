using MediatR;

namespace Stambat.Application.CQRS.Commands.Tenants;

public sealed record CancelInvitationCommand(Guid InvitationId) : IRequest<CancelInvitationCommandResult>;

public sealed record CancelInvitationCommandResult(string Message);
