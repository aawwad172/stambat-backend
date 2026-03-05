using Microsoft.Extensions.Logging;

using Stamply.Application.CQRS.Commands.Tenant;
using Stamply.Domain.Interfaces.Application.Services;
using Stamply.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stamply.Application.CQRS.CommandHandlers.Tenant;

public class InviteMerchantCommandHandler(
    ICurrentUserService currentUserService,
    ILogger<InviteMerchantCommandHandler> logger,
    IUnitOfWork unitOfWork)
    : BaseHandler<InviteMerchantCommand, InviteMerchantCommandResult>(currentUserService, logger, unitOfWork)
{
    public override Task<InviteMerchantCommandResult> Handle(
        InviteMerchantCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
