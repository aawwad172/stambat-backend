using MediatR;

using Stamply.Domain.Enums;

namespace Stamply.Application.CQRS.Commands.Tenants;

public sealed record InviteStaffCommand(string Email, RolesEnum Role) : IRequest<InviteStaffCommandResult>;

public sealed record InviteStaffCommandResult(string Message);
