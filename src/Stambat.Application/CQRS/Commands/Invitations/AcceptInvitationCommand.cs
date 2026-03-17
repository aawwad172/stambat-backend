using MediatR;

using Stambat.Domain.ValueObjects;

namespace Stambat.Application.CQRS.Commands.Invitations;

public sealed record AcceptInvitationCommand(
    FullName FullName,
    string Username,
    string Password,
    string Token) : IRequest<AcceptInvitationCommandResult>;

public sealed record AcceptInvitationCommandResult(Guid Id,
    FullName FullName,
    string Email,
    string Username,
    bool IsActive,
    bool IsVerified,
    string Message);
