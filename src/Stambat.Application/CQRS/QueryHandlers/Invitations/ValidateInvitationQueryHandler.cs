using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Queries.Invitations;
using Stambat.Domain.Entities;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.CQRS.QueryHandlers.Invitations;

public class ValidateInvitationQueryHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService currentTenantProviderService,
    ISecurityService securityService,
    ILogger<ValidateInvitationQueryHandler> logger,
    IUnitOfWork unitOfWork,
    IInvitationRepository invitationRepository,
    IUserRepository userRepository)
    : BaseHandler<ValidateInvitationQuery, ValidateInvitationQueryResult>(currentUserService, currentTenantProviderService, logger, unitOfWork)
{
    private readonly IInvitationRepository _invitationRepository = invitationRepository;
    private readonly ISecurityService _securityService = securityService;
    private readonly IUserRepository _userRepository = userRepository;

    public override async Task<ValidateInvitationQueryResult> Handle(ValidateInvitationQuery request, CancellationToken cancellationToken)
    {
        string tokenHash = _securityService.HashToken(request.Token);

        try
        {
            Invitation? invitation = await _invitationRepository.GetInvitationByTokenHashAsync(tokenHash);

            if (invitation is null)
                throw new NotFoundException($"Invitation with token not found");

            invitation.ValidateForUse();

            // Determine which flow the invitee should use based on whether they
            // already exist and whether they have credentials.
            User? existingUser = await _userRepository.GetUserByEmailAsync(invitation.Email);

            string inviteAction = existingUser switch
            {
                null => "register",
                { UserCredentialsId: not null } => "join",
                _ => "setup-credentials"
            };

            return new ValidateInvitationQueryResult(
                Email: invitation.Email,
                TenantName: invitation.Tenant!.BusinessName,
                RoleName: invitation.Role!.Name,
                InviteAction: inviteAction);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during validating invitation.");
            throw;
        }
    }
}
