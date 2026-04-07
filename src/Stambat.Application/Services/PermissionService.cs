using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.Services;

public class PermissionService(IAuthenticationRepository authenticationRepository) : IPermissionService
{
    private readonly IAuthenticationRepository _authenticationRepository = authenticationRepository;

    public async Task<List<string>> GetUserPermissionsAsync(User user)
    {
        // 1. --- Check for SuperAdmin (Business Rule) ---
        // Uses the repository to check if the user is a SuperAdmin.
        if (await _authenticationRepository.IsUserInRoleAsync(user.Id, "SuperAdmin"))
        {
            // SuperAdmin bypass: return all permissions defined in the system.
            return await _authenticationRepository.GetAllPermissionNamesAsync();
        }

        // 2. --- Calculate Base Permissions (from Roles) ---
        List<Guid> userRoleIds = await _authenticationRepository.GetUserRoleIdsAsync(user.Id);
        List<string> baseGrantedPermissions = await _authenticationRepository.GetBaseGrantedPermissionsAsync(userRoleIds.ToList());

        HashSet<string> finalPermissions = new(baseGrantedPermissions, StringComparer.OrdinalIgnoreCase);

        return finalPermissions.ToList();
    }

    public async Task<List<string>> GetUserRolesAsync(Guid userId)
    {
        // Simple passthrough to the repository
        return await _authenticationRepository.GetUserRolesAsync(userId);
    }

    // --- Tenant-scoped methods ---

    public async Task<List<string>> GetUserPermissionsForTenantAsync(User user, Guid tenantId)
    {
        // For tenant context, get only the roles assigned in that specific tenant
        List<Guid> tenantRoleIds = await _authenticationRepository.GetUserRoleIdsForTenantAsync(user.Id, tenantId);

        if (tenantRoleIds.Count == 0)
            return [];

        List<string> baseGrantedPermissions = await _authenticationRepository.GetBaseGrantedPermissionsAsync(tenantRoleIds);

        HashSet<string> finalPermissions = new(baseGrantedPermissions, StringComparer.OrdinalIgnoreCase);

        return finalPermissions.ToList();
    }

    public async Task<List<string>> GetUserRolesForTenantAsync(Guid userId, Guid tenantId)
    {
        return await _authenticationRepository.GetUserRolesForTenantAsync(userId, tenantId);
    }
}
