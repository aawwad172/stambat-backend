using MediatR;

namespace Stamply.Application.CQRS.Commands.Tenant;

public sealed record InviteTenantCommand(string Email) : IRequest<InviteTenantCommandResult>;

public sealed record InviteTenantCommandResult();
