using MediatR;

namespace Stambat.Application.CQRS.Commands.Admin;

public sealed record InviteTenantCommand(string Email) : IRequest<InviteTenantCommandResult>;

public sealed record InviteTenantCommandResult();
