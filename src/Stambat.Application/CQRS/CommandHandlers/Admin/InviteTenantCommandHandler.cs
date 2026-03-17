using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Admin;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.CQRS.CommandHandlers.Admin;

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
