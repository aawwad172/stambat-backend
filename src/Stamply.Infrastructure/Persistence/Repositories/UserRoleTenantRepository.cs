using Microsoft.EntityFrameworkCore;

using Stamply.Domain.Entities.Identity.Authentication;
using Stamply.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stamply.Infrastructure.Persistence.Repositories;

public class UserRoleTenantRepository(ApplicationDbContext context) : Repository<UserRoleTenant>(context), IUserRoleTenantRepository
{
    public async Task<UserRoleTenant?> GetUserRoleTenantAsync(Guid userId, Guid tenantId, Guid roleId)
    {
        return await _dbSet.FirstOrDefaultAsync(urt => urt.UserId == userId
                                                       && urt.TenantId == tenantId
                                                       && urt.RoleId == roleId);
    }
}
