using Microsoft.Extensions.Logging;

using Stamply.Application.CQRS.Commands.Tenant;
using Stamply.Domain.Interfaces.Application.Services;
using Stamply.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stamply.Application.CQRS.CommandHandlers.Tenant;

public class InviteTenantCommandHandler(
    ICurrentUserService currentUserService,
    ILogger<InviteTenantCommandHandler> logger,
    IUnitOfWork unitOfWork)
    : BaseHandler<InviteTenantCommand, InviteTenantCommandResult>(currentUserService, logger, unitOfWork)
{
    public override Task<InviteTenantCommandResult> Handle(
        InviteTenantCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
