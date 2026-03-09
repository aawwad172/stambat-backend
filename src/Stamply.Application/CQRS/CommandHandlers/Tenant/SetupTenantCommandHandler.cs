using Microsoft.Extensions.Logging;

using Stamply.Application.CQRS.Commands.Tenant;
using Stamply.Domain.Common;
using Stamply.Domain.Entities.Identity;
using Stamply.Domain.Entities.Identity.Authentication;
using Stamply.Domain.Exceptions;
using Stamply.Domain.Interfaces.Application.Services;
using Stamply.Domain.Interfaces.Infrastructure.IEmail;
using Stamply.Domain.Interfaces.Infrastructure.IRepositories;

using TenantEntity = Stamply.Domain.Entities.Tenant;

namespace Stamply.Application.CQRS.CommandHandlers.Tenant;

public class SetupTenantCommandHandler(
    ICurrentUserService currentUserService,
    ILogger<SetupTenantCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    ITenantRepository tenantRepository,
    IAuthenticationRepository authenticationRepository,
    IEmailService emailService)
    : BaseHandler<SetupTenantCommand, SetupTenantCommandResult>(currentUserService, logger, unitOfWork)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly ITenantRepository _tenantRepository = tenantRepository;
    private readonly IAuthenticationRepository _authenticationRepository = authenticationRepository;
    private readonly IEmailService _emailService = emailService;
    // Todo: Seed Tenant Admin Role into db and change this here
    private readonly string _tenantAdminRole = "User";
    public override async Task<SetupTenantCommandResult> Handle(
        SetupTenantCommand request,
        CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(_currentUser.UserId);
        if (user is null)
            throw new NotFoundException($"User with Id: {_currentUser.UserId} not found or empty");

        if (!user.IsVerified)
            throw new UserNotVerifiedException($"User with Id: {user.Id} is not verfied, please verify your email and try again.");

        TenantEntity? exisitingTenant = await _tenantRepository.GetTenantByEmailAsync(request.BusinessEmail);

        if (exisitingTenant is not null)
            throw new ConflictException($"Tenant with Email: {request.BusinessEmail} already exists");

        Role? tenantAdminRole = await _roleRepository.GetRoleByNameAsync(_tenantAdminRole);

        if (tenantAdminRole is null)
            // IMPORTANT: This prevents users from being created without a role if the DB isn't seeded.
            throw new InvalidOperationException($"The TenantAdmin role '{_tenantAdminRole}' does not exist in the database. Please seed roles.");

        Guid tenantId = Id.New();
        TenantEntity tenant = new()
        {
            Id = tenantId,
            BusinessName = request.CompanyName,
            Email = request.BusinessEmail,
            IsActive = true,
            IsDeleted = false
        };

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            await _tenantRepository.AddAsync(tenant);

            UserRoleTenant userRoleTenant = new()
            {
                Id = Id.New(),
                UserId = user.Id,
                RoleId = tenantAdminRole.Id,
                TenantId = tenantId
            };

            await _authenticationRepository.AddUserRoleTenantAsync(userRoleTenant);

            // Todo: add the FE dashboard link to both and add the role name as enum

            await _emailService.SendTeamInvitationEmailAsync(user.Email, user.FullName.FirstName, "Tenant Admin", tenant.BusinessName, "localhost:4200/dashboard");

            await _emailService.SendTenantWelcomeEmailAsync(tenant.Email, $"{user.FullName.FirstName} {user.FullName.LastName}", tenant.BusinessName, "localhost:4200/dashboard");

            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new SetupTenantCommandResult(tenant.Id, user.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred during tenant registration: {Message}", ex.Message);
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
