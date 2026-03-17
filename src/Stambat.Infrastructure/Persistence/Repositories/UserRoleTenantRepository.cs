using Microsoft.EntityFrameworkCore;

using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Infrastructure.Persistence.Repositories;

public class UserRoleTenantRepository(ApplicationDbContext context) : Repository<UserRoleTenant>(context), IUserRoleTenantRepository
{
    public async Task<UserRoleTenant?> GetUserRoleTenantAsync(Guid userId, Guid tenantId, Guid roleId)
    {
        return await _dbSet.FirstOrDefaultAsync(urt => urt.UserId == userId
                                                       && urt.TenantId == tenantId
                                                       && urt.RoleId == roleId);
    }
}
