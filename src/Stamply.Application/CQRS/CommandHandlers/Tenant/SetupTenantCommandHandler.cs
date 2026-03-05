using Microsoft.Extensions.Logging;

using Stamply.Application.CQRS.Commands.Tenant;
using Stamply.Domain.Common;
using Stamply.Domain.Entities.Identity;
using Stamply.Domain.Exceptions;
using Stamply.Domain.Interfaces.Application.Services;
using Stamply.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stamply.Application.CQRS.CommandHandlers.Tenant;

public class SetupTenantCommandHandler(
    ICurrentUserService currentUserService,
    ILogger<SetupTenantCommandHandler> logger,
    IUnitOfWork unitOfWork)
    : BaseHandler<SetupTenantCommand, SetupTenantCommandResult>(currentUserService, logger, unitOfWork)
{
    public override Task<SetupTenantCommandResult> Handle(
        SetupTenantCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
