using Microsoft.Extensions.Logging;

using Stamply.Application.CQRS.Queries.Tenant;
using Stamply.Domain.Entities;
using Stamply.Domain.Exceptions;
using Stamply.Domain.Interfaces.Application.Services;
using Stamply.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stamply.Application.CQRS.QueryHandlers.Tenant;

public class ValidateInvitationQueryHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService currentTenantProviderService,
    ISecurityService securityService,
    ILogger<ValidateInvitationQueryHandler> logger,
    IUnitOfWork unitOfWork,
    IInvitationRepository invitationRepository)
    : BaseHandler<ValidateInvitationQuery, ValidateInvitationQueryResult>(currentUserService, currentTenantProviderService, logger, unitOfWork)
{
    private readonly IInvitationRepository _invitationRepository = invitationRepository;
    private readonly ISecurityService _securityService = securityService;

    public override async Task<ValidateInvitationQueryResult> Handle(ValidateInvitationQuery request, CancellationToken cancellationToken)
    {
        string tokenHash = _securityService.HashToken(request.Token);

        try
        {
            Invitation? inviation = await _invitationRepository.GetInvitationByTokenHashAsync(tokenHash);

            if (inviation is null)
                throw new NotFoundException($"Invitation with token not found");

            if (inviation.IsUsed)
                throw new InvitationExpiredException($"Invitation with token already used");

            if (inviation.ExpiresAt < DateTime.UtcNow)
                throw new InvitationExpiredException($"Invitation with token expired");


            return new ValidateInvitationQueryResult(Email: inviation.Email, TenantName: inviation.Tenant!.BusinessName, RoleName: inviation.Role.Name);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during validating invitation.");
            throw;
        }
    }
}
