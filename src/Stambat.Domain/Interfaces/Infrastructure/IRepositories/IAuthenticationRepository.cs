namespace Stambat.Domain.Interfaces.Infrastructure.IRepositories;

public interface IAuthenticationRepository
{
    Task<List<string>> GetUserRolesAsync(Guid userId);

    // Permission Retrieval
    Task<List<string>> GetBaseGrantedPermissionsAsync(List<Guid> roleIds);
    // Helper
    Task<List<Guid>> GetUserRoleIdsAsync(Guid userId);
    Task<bool> IsUserInRoleAsync(Guid userId, string roleName);
    Task<List<string>> GetAllPermissionNamesAsync();

    // Tenant-scoped methods
    Task<List<Guid>> GetUserRoleIdsForTenantAsync(Guid userId, Guid tenantId);
    Task<List<string>> GetUserRolesForTenantAsync(Guid userId, Guid tenantId);
    Task<List<TenantInfo>> GetUserTenantsAsync(Guid userId);
}

public record TenantInfo(Guid TenantId, string BusinessName, List<string> Roles);
