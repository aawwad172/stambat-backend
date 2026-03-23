using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Tenants;
using Stambat.Domain.Common;
using Stambat.Domain.Entities;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IEmail;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;


namespace Stambat.Application.CQRS.CommandHandlers.Tenants;

public class InviteStaffCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<InviteStaffCommandHandler> logger,
    IUnitOfWork unitOfWork,
    ISecurityService securityService,
    IEmailService emailService,
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IInvitationRepository invitationRepository,
    IRepository<Tenant> tenantRepository)
    : BaseHandler<InviteStaffCommand, InviteStaffCommandResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly ISecurityService _securityService = securityService;
    private readonly IEmailService _emailService = emailService;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IInvitationRepository _invitationRepository = invitationRepository;
    private readonly IRepository<Tenant> _tenantRepository = tenantRepository;
    private const int _invitationExpiresAfter = 1;

    public override async Task<InviteStaffCommandResult> Handle(
        InviteStaffCommand request,
        CancellationToken cancellationToken)
    {
        Guid? optionalTenantId = _currentTenant.TenantId;

        if (!optionalTenantId.HasValue)
            throw new ArgumentException("TenantId can't be null, please provide it via the X-Tenant-Id header.", nameof(_currentTenant.TenantId));

        Guid validTenantId = optionalTenantId.Value;

        // 1. Basic Validations
        User? user = await _userRepository.GetUserByEmailAsync(request.Email);
        Role? role = await _roleRepository.GetRoleByNameAsync(request.Role.ToString());

        Tenant? tenant = await _tenantRepository.GetByIdAsync(validTenantId);

        if (tenant is null)
            throw new NotFoundException($"Tenant: {validTenantId} was not found");

        if (role is null)
            throw new NotFoundException($"Role: {request.Role} was not found");

        if (user is not null)
        {
            if (user.UserRoleTenants.Any(urt => urt.TenantId == validTenantId && urt.RoleId == role.Id))
                throw new ConflictException("User already holds this role in this tenant.");
        }

        // 2. Token Generation
        // Create a secure random string (raw token)
        string rawToken = IdGenerator.New().ToString("N");

        // Hash the token for DB storage (one-way hash lSHA256)
        string hashedToken = _securityService.HashToken(rawToken);

        Invitation invitation = Invitation.Create(
            request.Email,
            hashedToken,
            rawToken,
            role.Id,
            validTenantId,
            DateTime.UtcNow.AddDays(_invitationExpiresAfter)
        );

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            // Check if an active invitation already exists
            bool alreadyExists = await _invitationRepository.ExistsActiveAsync(
                invitation.Email, validTenantId, invitation.RoleId, cancellationToken);

            await _invitationRepository.AddAsync(invitation);

            if (alreadyExists)
                throw new InvitationStillActiveException($"Invitation for {role.Name} is still active.");

            // 3. Construct the Invitation Link
            // todo: In a real scenario, pull "https://stambat.app/register" from IConfiguration
            string registrationLink = $"https://stambat.app/register?token={Uri.EscapeDataString(rawToken)}";

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


            return new InviteStaffCommandResult("Invitation Sent Successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred during staff invitation: {Message}", ex.Message);
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
