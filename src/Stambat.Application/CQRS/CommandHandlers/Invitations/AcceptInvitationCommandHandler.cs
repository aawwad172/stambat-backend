using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Invitations;
using Stambat.Domain.Common;
using Stambat.Domain.Entities;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IEmail;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;
using Stambat.Domain.ValueObjects;


namespace Stambat.Application.CQRS.CommandHandlers.Invitations;

public class AcceptInvitationCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService currentTenantProviderService,
    ILogger<AcceptInvitationCommandHandler> logger,
    IUnitOfWork unitOfWork,
    ISecurityService securityService,
    IEmailService emailService,
    IUserRepository userRepository,
    IInvitationRepository invitationRepository)
    : BaseHandler<AcceptInvitationCommand, AcceptInvitationCommandResult>(currentUserService, currentTenantProviderService, logger, unitOfWork)
{
    private readonly ISecurityService _securityService = securityService;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IInvitationRepository _invitationRepository = invitationRepository;
    private readonly IEmailService _emailService = emailService;

    public override async Task<AcceptInvitationCommandResult> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
    {
        string tokenHash = _securityService.HashToken(request.Token);
        Invitation? invitation = await _invitationRepository.GetInvitationByTokenHashAsync(tokenHash);

        if (invitation is null)
            throw new NotFoundException("Invitation not found.");

        if (invitation.IsUsed)
            throw new InvitationExpiredException("This invitation has already been used.");

        if (invitation.ExpiresAt < DateTime.UtcNow)
            throw new InvitationExpiredException("This invitation has expired.");

        if (invitation.Role is null)
        {
            // IMPORTANT: This prevents users from being created without a role if the DB isn't seeded.
            throw new InvalidOperationException($"The role with Id: {invitation.RoleId} does not exist in the database. Please seed roles.");
        }

        if (invitation.Tenant is null)
        {
            throw new NotFoundException($"The tenant with Id: {invitation.TenantId} does not exist.");
        }

        invitation.MarkAsUsed();

        // 2. Business Logic: Existing User Handling
        User? user = await _userRepository.GetUserByEmailAsync(invitation.Email);
        bool isNewUser = user is null;


        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            _invitationRepository.Update(invitation);
            if (isNewUser)
            {
                // Check if a user already exists with the same username.
                User? existingUsername = await _userRepository.GetUserByUsernameAsync(request.Username);
                if (existingUsername is not null)
                    throw new ConflictException("A user with this username already exists.");

                // Hash the password using the security service (BCrypt)
                string hashedPassword = _securityService.HashSecret(request.Password);

                // Generate a new security stamp
                string securityStamp = IdGenerator.New().ToString();


                user = User.Create(
                    FullName.Create(request.FirstName, request.LastName, request.MiddleName),
                    request.Username,
                    Email.Create(invitation.Email),
                    securityStamp,
                    isVerified: true
                );

                UserCredentials userCreds = UserCredentials.Create(user.Id, hashedPassword);

                user.SetCredentials(userCreds);

                await _userRepository.AddAsync(user);
            }

            Guid verifiedTenantId = invitation.TenantId!.Value;

            user!.AssignRole(invitation.RoleId, verifiedTenantId);

            _userRepository.Update(user);


            // Todo: add the FE dashboard link to both and add the role name as enum
            await _emailService.SendExistingUserAccessGrantAsync(
                user.Email.Value,
                user.FullName.FirstName,
                invitation.Role.Name,
                invitation.Tenant.BusinessName,
                "localhost:4200/dashboard");


            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new AcceptInvitationCommandResult(
                Id: user.Id,
                FullName: user.FullName,
                Email: user.Email.Value,
                Username: user.Username,
                IsActive: user.IsActive,
                IsVerified: user.IsVerified,
                Message: "User registered successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred during registration: {Message}", ex.Message);
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
