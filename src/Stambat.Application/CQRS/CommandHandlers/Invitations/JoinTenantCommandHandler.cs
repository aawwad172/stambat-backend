using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Invitations;
using Stambat.Domain.Entities;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IEmail;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.CQRS.CommandHandlers.Invitations;

public class JoinTenantCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService currentTenantProviderService,
    ILogger<JoinTenantCommandHandler> logger,
    IUnitOfWork unitOfWork,
    ISecurityService securityService,
    IEmailService emailService,
    IUserRepository userRepository,
    IInvitationRepository invitationRepository)
    : BaseHandler<JoinTenantCommand, JoinTenantCommandResult>(currentUserService, currentTenantProviderService, logger, unitOfWork)
{
    private readonly ISecurityService _securityService = securityService;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IInvitationRepository _invitationRepository = invitationRepository;
    private readonly IEmailService _emailService = emailService;

    public override async Task<JoinTenantCommandResult> Handle(JoinTenantCommand request, CancellationToken cancellationToken)
    {
        string tokenHash = _securityService.HashToken(request.Token);
        Invitation? invitation = await _invitationRepository.GetInvitationByTokenHashAsync(tokenHash);

        if (invitation is null)
            throw new NotFoundException("Invitation not found.");

        invitation.ValidateForUse();

        // This endpoint is for existing users only
        User? user = await _userRepository.GetUserByEmailAsync(invitation.Email);
        if (user is null)
            throw new NotFoundException("No account found with this email. Use the /invitations/accept endpoint to create a new account.");

        // Verify identity: the user must provide their current password
        if (user.Credentials is null ||
            !_securityService.VerifySecret(request.Password, user.Credentials.PasswordHash))
        {
            throw new UnauthenticatedException(
                "Invalid password. Please use your existing account password to confirm your identity.");
        }

        invitation.MarkAsUsed();

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            _invitationRepository.Update(invitation);

            Guid verifiedTenantId = invitation.TenantId!.Value;

            user.AssignRole(invitation.RoleId, verifiedTenantId);

            _userRepository.Update(user);

            // Notify user they've been granted access
            await _emailService.SendExistingUserAccessGrantAsync(
                user.Email.Value,
                user.FullName.FirstName,
                invitation.Role!.Name,
                invitation.Tenant!.BusinessName,
                "localhost:4200/dashboard");

            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new JoinTenantCommandResult(
                TenantId: verifiedTenantId,
                TenantName: invitation.Tenant!.BusinessName,
                RoleName: invitation.Role!.Name,
                Message: $"Successfully joined {invitation.Tenant.BusinessName}");
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred during tenant join: {Message}", ex.Message);
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
