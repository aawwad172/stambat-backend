using MediatR;

using Stamply.Domain.Enums;

namespace Stamply.Application.CQRS.Commands.Tenant;

public sealed record InviteMerchantCommand(string Email, RolesEnum Role) : IRequest<InviteMerchantCommandResult>;

public sealed record InviteMerchantCommandResult(string Message);
