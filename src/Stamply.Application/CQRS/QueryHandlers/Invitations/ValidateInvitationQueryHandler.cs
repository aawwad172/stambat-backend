using Microsoft.Extensions.Logging;

using Stamply.Application.CQRS.Queries.Invitations;
using Stamply.Domain.Entities;
using Stamply.Domain.Exceptions;
using Stamply.Domain.Interfaces.Application.Services;
using Stamply.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stamply.Application.CQRS.QueryHandlers.Invitations;

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
            Invitation? invitation = await _invitationRepository.GetInvitationByTokenHashAsync(tokenHash);

            if (invitation is null)
                throw new NotFoundException($"Invitation with token not found");

            if (invitation.IsUsed)
                throw new InvitationExpiredException($"Invitation with token already used");

            if (invitation.ExpiresAt < DateTime.UtcNow)
                throw new InvitationExpiredException($"Invitation with token expired");

            if (invitation.Tenant is null)
                throw new NotFoundException($"The tenant with Id: {invitation.TenantId} does not exist.");

            if (invitation.Role is null)
                throw new NotFoundException($"The role with Id: {invitation.RoleId} does not exist.");


            return new ValidateInvitationQueryResult(Email: invitation.Email, TenantName: invitation.Tenant.BusinessName, RoleName: invitation.Role.Name);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during validating invitation.");
            throw;
        }
    }
}
