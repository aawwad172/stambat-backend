using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Tenants;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.CQRS.CommandHandlers.Tenants;

public class RemoveStaffCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<RemoveStaffCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository)
    : BaseHandler<RemoveStaffCommand, RemoveStaffCommandResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly IUserRepository _userRepository = userRepository;

    public override async Task<RemoveStaffCommandResult> Handle(RemoveStaffCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            if (_currentTenant.TenantId is null)
                throw new ArgumentException("TenantId should be provided via JWT claims.");

            Guid tenantId = _currentTenant.TenantId.Value;

            User user = await _userRepository.GetStaffMemberByTenantAsync(tenantId, request.StaffId)
                ?? throw new NotFoundException($"Staff member {request.StaffId} was not found in this tenant.");

            List<UserRoleTenant> tenantRoles = [.. user.UserRoleTenants.Where(urt => urt.TenantId == tenantId)];

            foreach (UserRoleTenant urt in tenantRoles)
                user.RemoveRole(urt.RoleId, tenantId);

            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new RemoveStaffCommandResult("Staff member removed from tenant successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while removing staff member: {Message}", ex.Message);
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
