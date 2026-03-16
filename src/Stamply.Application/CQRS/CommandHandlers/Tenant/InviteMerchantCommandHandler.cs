using Microsoft.Extensions.Logging;

using Stamply.Application.CQRS.Commands.Tenant;
using Stamply.Domain.Common;
using Stamply.Domain.Entities;
using Stamply.Domain.Entities.Identity;
using Stamply.Domain.Entities.Identity.Authentication;
using Stamply.Domain.Exceptions;
using Stamply.Domain.Interfaces.Application.Services;
using Stamply.Domain.Interfaces.Infrastructure.IEmail;
using Stamply.Domain.Interfaces.Infrastructure.IRepositories;

using TenantEntity = Stamply.Domain.Entities.Tenant;

namespace Stamply.Application.CQRS.CommandHandlers.Tenant;

public class InviteMerchantCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<InviteMerchantCommandHandler> logger,
    IUnitOfWork unitOfWork,
    ISecurityService securityService,
    IEmailService emailService,
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IUserRoleTenantRepository userRoleTenantRepository,
    IInvitationRepository invitationRepository,
    IRepository<TenantEntity> tenantRepository)
    : BaseHandler<InviteMerchantCommand, InviteMerchantCommandResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly ISecurityService _securityService = securityService;
    private readonly IEmailService _emailService = emailService;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IUserRoleTenantRepository _userRoleTenantRepository = userRoleTenantRepository;
    private readonly IInvitationRepository _invitationRepository = invitationRepository;
    private readonly IRepository<TenantEntity> _tenantRepository = tenantRepository;
    private const int _invitationExpiresAfter = 1;

    public override async Task<InviteMerchantCommandResult> Handle(
        InviteMerchantCommand request,
        CancellationToken cancellationToken)
    {
        Guid? optionalTenantId = _currentTenant.TenantId;

        if (!optionalTenantId.HasValue)
            throw new ArgumentNullException("TenantId can't be null, please provide it via the X-Tenant-Id header.");

        Guid validTenantId = optionalTenantId.Value;

        // 1. Basic Validations
        User? user = await _userRepository.GetUserByUsernameAsync(request.Email);
        Role? role = await _roleRepository.GetRoleByNameAsync(request.Role.ToString());
        TenantEntity? tenant = await _tenantRepository.GetByIdAsync(validTenantId);

        if (tenant is null)
            throw new NotFoundException($"Tenant: {validTenantId} was not found");

        if (role is null)
            throw new NotFoundException($"Role: {request.Role} was not found");

        if (user is not null)
        {
            UserRoleTenant? userRoleTenant = await _userRoleTenantRepository.GetUserRoleTenantAsync(user.Id, validTenantId, role.Id);
            if (userRoleTenant is not null)
                throw new ConflictException("User already holds this role in this tenant.");
        }

        // Check if there is an active invitation for that user in that tenant for that role
        Invitation? lastActiveInvitation = await _invitationRepository.GetLastActiveInvitationForTenantAndRole(request.Email, validTenantId, role.Id);

        if (lastActiveInvitation is not null)
            throw new InvitationStillActiveException($"Invitation already sent with role: {role.Name} for tenant: {tenant.BusinessName} and it is still active");

        // 2. Token Generation
        // Create a secure random string (raw token)
        string rawToken = Id.New().ToString("N");

        // Hash the token for DB storage (one-way hash lSHA256)
        string hashedToken = _securityService.HashToken(rawToken);

        Invitation invitation = new()
        {
            Id = Id.New(),
            Email = request.Email,
            RoleId = role.Id,
            TenantId = validTenantId,
            TokenHash = hashedToken,
            ExpiresAt = DateTime.UtcNow.AddDays(_invitationExpiresAfter),
            IsUsed = false,
        };

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            await _invitationRepository.AddAsync(invitation);

            // 3. Construct the Invitation Link
            // todo: In a real scenario, pull "https://stamply.app/register" from IConfiguration
            string registrationLink = $"https://stamply.app/register?token={rawToken}&email={request.Email}";

            // 4. Send the Email
            // Assuming your IEmailService has a SendInvitationEmailAsync method
            await _emailService.SendMerchantOnboardingInviteAsync(
                request.Email,
                tenant.BusinessName,
                registrationLink,
                _invitationExpiresAfter,
                cancellationToken);

            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new InviteMerchantCommandResult("Invitation Sent Successfully");
        }
        catch (Exception ex)
        {
            logger.LogError("An error occurred during merchant invitation: {Message}", ex.Message);
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
