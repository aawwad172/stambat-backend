using Microsoft.Extensions.Logging;

using Stamply.Application.CQRS.Commands.Admin;
using Stamply.Domain.Interfaces.Application.Services;
using Stamply.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stamply.Application.CQRS.CommandHandlers.Admin;

public class InviteTenantCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<InviteTenantCommandHandler> logger,
    IUnitOfWork unitOfWork)
    : BaseHandler<InviteTenantCommand, InviteTenantCommandResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    public override Task<InviteTenantCommandResult> Handle(
        InviteTenantCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
