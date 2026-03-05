using MediatR;

namespace Stamply.Application.CQRS.Commands.Tenant;

public sealed record InviteMerchantCommand : IRequest<InviteMerchantCommandResult>;

public sealed record InviteMerchantCommandResult();
