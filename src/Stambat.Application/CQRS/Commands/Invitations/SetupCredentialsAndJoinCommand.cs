using MediatR;

namespace Stambat.Application.CQRS.Commands.Invitations;

public sealed record SetupCredentialsAndJoinCommand(
    string Username,
    string Password,
    string Token) : IRequest<SetupCredentialsAndJoinCommandResult>;

public sealed record SetupCredentialsAndJoinCommandResult(
    Guid TenantId,
    string TenantName,
    string RoleName,
    string Message);
