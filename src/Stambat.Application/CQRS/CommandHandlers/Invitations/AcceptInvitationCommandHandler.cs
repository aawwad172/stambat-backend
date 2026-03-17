using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Invitations;
using Stambat.Domain.Common;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IEmail;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;


namespace Stambat.Application.CQRS.CommandHandlers.Invitations;

public class AcceptInvitationCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService currentTenantProviderService,
    ILogger<AcceptInvitationCommandHandler> logger,
    IUnitOfWork unitOfWork,
    ISecurityService securityService,
    IEmailService emailService,
    IUserRepository userRepository,
    IInvitationRepository invitationRepository,
    IRepository<UserCredentials> userCredentialsRepository,
    IRepository<UserToken> userTokenRepository,
    IAuthenticationRepository authenticationRepository)
    : BaseHandler<AcceptInvitationCommand, AcceptInvitationCommandResult>(currentUserService, currentTenantProviderService, logger, unitOfWork)
{
    private readonly ISecurityService _securityService = securityService;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IInvitationRepository _invitationRepository = invitationRepository;
    private readonly IRepository<UserCredentials> _userCredentialsRepository = userCredentialsRepository;
    private readonly IEmailService _emailService = emailService;
    private readonly IRepository<UserToken> _userTokenRepository = userTokenRepository;
    private readonly IAuthenticationRepository _authenticationRepository = authenticationRepository;

    public override async Task<AcceptInvitationCommandResult> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
    {
        string tokenHash = _securityService.HashToken(request.Token);
        var invitation = await _invitationRepository.GetInvitationByTokenHashAsync(tokenHash);

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

        invitation.IsUsed = true;

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
                string securityStamp = Id.New().ToString();

                Guid id = Id.New();

                UserCredentials userCreds = new()
                {
                    Id = Id.New(),
                    UserId = id,
                    PasswordHash = hashedPassword

                };

                user = new()
                {
                    Id = id,
                    FullName = request.FullName,
                    Email = invitation.Email,
                    Username = request.Username,
                    IsActive = true,
                    IsDeleted = false,
                    IsVerified = true,
                    SecurityStamp = securityStamp,
                    UserCredentialsId = userCreds.Id
                };
                await _userCredentialsRepository.AddAsync(userCreds);
                await _userRepository.AddAsync(user);
            }

            UserRoleTenant userRoleTenant = new()
            {
                Id = Id.New(),
                UserId = user!.Id,
                RoleId = invitation.RoleId,
                TenantId = invitation.TenantId
            };

            await _authenticationRepository.AddUserRoleTenantAsync(userRoleTenant);


            // Todo: add the FE dashboard link to both and add the role name as enum
            await _emailService.SendExistingUserAccessGrantAsync(
                user.Email,
                user.FullName.FirstName,
                invitation.Role.Name,
                invitation.Tenant.BusinessName,
                "localhost:4200/dashboard");


            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new AcceptInvitationCommandResult(
                Id: user.Id,
                FullName: user.FullName,
                Email: user.Email,
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
