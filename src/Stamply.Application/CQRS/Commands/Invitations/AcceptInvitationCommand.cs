using MediatR;

using Stamply.Domain.ValueObjects;

namespace Stamply.Application.CQRS.Commands.Invitations;

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
