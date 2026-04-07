using MediatR;

namespace Stambat.Application.CQRS.Commands.Invitations;

public sealed record JoinTenantCommand(
    string Password,
    string Token) : IRequest<JoinTenantCommandResult>;

public sealed record JoinTenantCommandResult(
    Guid TenantId,
    string TenantName,
    string RoleName,
    string Message);
