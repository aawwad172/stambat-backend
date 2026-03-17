using Microsoft.EntityFrameworkCore;

using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Infrastructure.Persistence.Repositories;

public class AuthenticationRepository(ApplicationDbContext dbContext) : IAuthenticationRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task AddUserRoleTenantAsync(UserRoleTenant userRole)
    {
        await _dbContext.UserRoleTenants.AddAsync(userRole);
    }

    public async Task<List<string>> GetAllPermissionNamesAsync()
    {
        return await _dbContext.Permissions
               .Select(p => p.Name)
               .ToListAsync();
    }

    public async Task<List<string>> GetBaseGrantedPermissionsAsync(List<Guid> roleIds)
    {
        // Query RolePermissions for all permissions granted by the user's roles
        return await _dbContext.RolePermissions
            .Where(rp => roleIds.Contains(rp.RoleId))
            .Select(rp => rp.Permission!.Name)
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<Guid>> GetUserRoleIdsAsync(Guid userId)
    {
        return await _dbContext.UserRoleTenants
               .Where(ur => ur.UserId == userId)
               .Select(ur => ur.RoleId)
               .ToListAsync();
    }

    public async Task<List<string>> GetUserRolesAsync(Guid userId)
    {
        return await _dbContext.UserRoleTenants
               .Where(ur => ur.UserId == userId)
               .Select(ur => ur.Role!.Name) // EF Core translates this join efficiently
               .ToListAsync();
    }

    public async Task<bool> IsUserInRoleAsync(Guid userId, string roleName)
    {
        return await _dbContext.UserRoleTenants
                .AnyAsync(ur => ur.UserId == userId && ur.Role!.Name == roleName);
    }
}
