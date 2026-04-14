using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Invitations;
using Stambat.Domain.Entities;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IEmail;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;


namespace Stambat.Application.CQRS.CommandHandlers.Invitations;

public class SetupCredentialsAndJoinCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService currentTenantProviderService,
    ILogger<SetupCredentialsAndJoinCommandHandler> logger,
    IUnitOfWork unitOfWork,
    ISecurityService securityService,
    IEmailService emailService,
    IUserRepository userRepository,
    IInvitationRepository invitationRepository)
    : BaseHandler<SetupCredentialsAndJoinCommand, SetupCredentialsAndJoinCommandResult>(currentUserService, currentTenantProviderService, logger, unitOfWork)
{
    private readonly ISecurityService _securityService = securityService;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IInvitationRepository _invitationRepository = invitationRepository;
    private readonly IEmailService _emailService = emailService;

    public override async Task<SetupCredentialsAndJoinCommandResult> Handle(SetupCredentialsAndJoinCommand request, CancellationToken cancellationToken)
    {
        string tokenHash = _securityService.HashToken(request.Token);
        Invitation invitation = await _invitationRepository.GetInvitationByTokenHashAsync(tokenHash)
            ?? throw new NotFoundException("Invitation not found.");

        invitation.ValidateForUse();

        // This endpoint is for existing credential-less users (e.g. customers who joined a loyalty card)
        User user = await _userRepository.GetUserByEmailAsync(invitation.Email)
            ?? throw new NotFoundException("No account found with this email. Use the /invitations/accept endpoint to create a new account.");

        if (user.Credentials is not null)
            throw new ConflictException("User already has credentials. Use the /invitations/join endpoint instead.");

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            // Validate username uniqueness only if it differs from the user's current username
            if (!string.Equals(user.Username, request.Username, StringComparison.Ordinal))
            {
                User? existingUsername = await _userRepository.GetUserByUsernameAsync(request.Username);
                if (existingUsername is not null)
                    throw new ConflictException("A user with this username already exists.");

                user.UpdateUsername(request.Username);
            }

            string hashedPassword = _securityService.HashSecret(request.Password);
            UserCredentials userCreds = UserCredentials.Create(user.Id, hashedPassword);
            user.SetCredentials(userCreds);

            Guid verifiedTenantId = invitation.TenantId!.Value;
            user.AssignRole(invitation.RoleId, verifiedTenantId);

            invitation.MarkAsUsed();
            _invitationRepository.Update(invitation);
            _userRepository.Update(user);

            await _emailService.SendExistingUserAccessGrantAsync(
                user.Email.Value,
                user.FullName.FirstName,
                invitation.Role!.Name,
                invitation.Tenant!.BusinessName,
                "localhost:4200/dashboard");

            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new SetupCredentialsAndJoinCommandResult(
                TenantId: verifiedTenantId,
                TenantName: invitation.Tenant!.BusinessName,
                RoleName: invitation.Role!.Name,
                Message: $"Credentials set and successfully joined {invitation.Tenant.BusinessName}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during credentials setup and tenant join: {Message}", ex.Message);
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
