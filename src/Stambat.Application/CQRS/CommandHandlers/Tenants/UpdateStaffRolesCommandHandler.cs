using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Tenants;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.CQRS.CommandHandlers.Tenants;

public class UpdateStaffRolesCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<UpdateStaffRolesCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository)
    : BaseHandler<UpdateStaffRolesCommand, UpdateStaffRolesCommandResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly IUserRepository _userRepository = userRepository;

    public override async Task<UpdateStaffRolesCommandResult> Handle(UpdateStaffRolesCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            if (_currentTenant.TenantId is null)
                throw new ArgumentNullException("TenantId should be provided via JWT claims");

            Guid tenantId = _currentTenant.TenantId.Value;

            User? user = await _userRepository.GetStaffMemberByTenantAsync(tenantId, request.StaffId);

            if (user is null)
                throw new NotFoundException($"Staff member {request.StaffId} was not found in this tenant.");

            foreach (RoleSelection selection in request.Roles)
            {
                bool alreadyHasRole = user.UserRoleTenants.Any(urt =>
                urt.TenantId == tenantId
                && urt.RoleId == selection.RoleId);

                if (selection.IsSelected && !alreadyHasRole)
                    user.AssignRole(selection.RoleId, tenantId);
                else if (!selection.IsSelected && alreadyHasRole)
                    user.RemoveRole(selection.RoleId, tenantId);
            }

            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new UpdateStaffRolesCommandResult("Staff roles updated successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating staff roles: {Message}", ex.Message);
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
