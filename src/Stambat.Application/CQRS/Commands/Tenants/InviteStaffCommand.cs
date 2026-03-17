using MediatR;

using Stambat.Domain.Enums;

namespace Stambat.Application.CQRS.Commands.Tenants;

public sealed record InviteStaffCommand(string Email, RolesEnum Role) : IRequest<InviteStaffCommandResult>;

public sealed record InviteStaffCommandResult(string Message);
