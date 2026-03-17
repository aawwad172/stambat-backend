using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Users;
using Stambat.Domain.Common;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Enums;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IEmail;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;
using Stambat.Domain.ValueObjects;

using UserEntity = Stambat.Domain.Entities.Identity.User;


namespace Stambat.Application.CQRS.CommandHandlers.Users;

public class RegisterUserCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<RegisterUserCommandHandler> logger,
    IAuthenticationRepository authenticationRepository,
    IUserRepository userRepository,
    ISecurityService securityService,
    IRoleRepository roleRepository,
    IRepository<UserCredentials> userCredentialsRepository,
    IRepository<UserToken> userTokenRepository,
    IUnitOfWork unitOfWork,
    IEmailService emailService)
    : BaseHandler<RegisterUserCommand, RegisterUserCommandResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly IAuthenticationRepository _authenticationRepository = authenticationRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRepository<UserCredentials> _userCredentialsRepository = userCredentialsRepository;
    private readonly IRepository<UserToken> _userTokenRepository = userTokenRepository;
    private readonly ISecurityService _securityService = securityService;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IEmailService _emailService = emailService;
    private readonly RolesEnum _defaultRole = RolesEnum.User;

    public override async Task<RegisterUserCommandResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Check if a user already exists with the same email.
        UserEntity? existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
        if (existingUser is not null)
            throw new ConflictException("A user with this email already exists.");

        // Check if a user already exists with the same username.
        UserEntity? existingUsername = await _userRepository.GetUserByUsernameAsync(request.Username);
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

        Role? defaultRole = await _roleRepository.GetRoleByNameAsync(_defaultRole.ToString());

        if (defaultRole is null)
        {
            // IMPORTANT: This prevents users from being created without a role if the DB isn't seeded.
            throw new InvalidOperationException($"The default role '{_defaultRole}' does not exist in the database. Please seed roles.");
        }

        UserEntity user = new()
        {
            Id = id,
            FullName = new FullName
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName
            },
            Email = request.Email,
            Username = request.Username,
            IsActive = true,
            IsDeleted = false,
            IsVerified = false,
            SecurityStamp = securityStamp,
            UserCredentialsId = userCreds.Id
        };

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _userCredentialsRepository.AddAsync(userCreds);
            await _userRepository.AddAsync(user);

            UserRoleTenant userRoleTenant = new()
            {
                Id = Id.New(),
                UserId = user.Id,
                RoleId = defaultRole.Id,
                TenantId = null // Null until he enters his business data
            };

            await _authenticationRepository.AddUserRoleTenantAsync(userRoleTenant);

            string verificationToken = Id.New().ToString("N");
            UserToken userToken = new()
            {
                Id = Id.New(),
                UserId = user.Id,
                Token = verificationToken,
                Type = UserTokenType.EmailVerification,
                ExpiryDate = DateTime.UtcNow.AddHours(24),
                IsUsed = false
            };

            await _userTokenRepository.AddAsync(userToken);

            // TODO: use the correct domain (the FE one)
            string verificationLink = $"https://stambat.app/verify-email?token={verificationToken}&userId={user.Id}";

            await _emailService.SendVerificationEmailAsync(
                to: request.Email,
                userName: request.Username,
                link: verificationLink);


            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new RegisterUserCommandResult(
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
