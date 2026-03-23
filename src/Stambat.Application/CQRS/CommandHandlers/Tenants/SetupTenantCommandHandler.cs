using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Tenants;
using Stambat.Domain.Common;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Enums;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IEmail;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

using TenantEntity = Stambat.Domain.Entities.Tenant;

namespace Stambat.Application.CQRS.CommandHandlers.Tenants;

public class SetupTenantCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<SetupTenantCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    ITenantRepository tenantRepository,
    IEmailService emailService)
    : BaseHandler<SetupTenantCommand, SetupTenantCommandResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly ITenantRepository _tenantRepository = tenantRepository;
    private readonly IEmailService _emailService = emailService;
    private readonly RolesEnum _tenantAdminRole = RolesEnum.TenantAdmin;
    public override async Task<SetupTenantCommandResult> Handle(
        SetupTenantCommand request,
        CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdWithDetailsAsync(_currentUser.UserId);
        if (user is null)
            throw new NotFoundException($"User with Id: {_currentUser.UserId} not found or empty");

        if (!user.IsVerified)
            throw new UserNotVerifiedException($"User with Id: {user.Id} is not verfied, please verify your email and try again.");

        TenantEntity? exisitingTenant = await _tenantRepository.GetTenantByEmailAsync(request.BusinessEmail);

        if (exisitingTenant is not null)
            throw new ConflictException($"Tenant with Email: {request.BusinessEmail} already exists");

        Role? tenantAdminRole = await _roleRepository.GetRoleByNameAsync(_tenantAdminRole.ToString());

        if (tenantAdminRole is null)
            // IMPORTANT: This prevents users from being created without a role if the DB isn't seeded.
            throw new InvalidOperationException($"The TenantAdmin role '{_tenantAdminRole}' does not exist in the database. Please seed roles.");

        Guid tenantId = IdGenerator.New();
        TenantEntity tenant = TenantEntity.Create(
            tenantId,
            request.CompanyName,
            request.BusinessEmail
        );

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            await _tenantRepository.AddAsync(tenant);

            user.AssignRole(tenantAdminRole.Id, tenantId);

            // Todo: add the FE dashboard link to both and add the role name as enum
            await _emailService.SendExistingUserAccessGrantAsync(
                user.Email,
                user.FullName.FirstName,
                "Tenant Admin",
                tenant.BusinessName,
                "localhost:4200/dashboard");

            await _emailService.SendTenantWelcomeEmailAsync(
                tenant.Email,
                $"{user.FullName.FirstName} {user.FullName.LastName}",
                tenant.BusinessName,
                "localhost:4200/dashboard");

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
