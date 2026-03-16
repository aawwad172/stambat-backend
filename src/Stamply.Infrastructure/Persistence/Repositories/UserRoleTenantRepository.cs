using System;

using Stamply.Domain.Entities.Identity.Authentication;
using Stamply.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stamply.Infrastructure.Persistence.Repositories;

public class UserRoleTenantRepository(ApplicationDbContext context) : Repository<UserRoleTenant>(context), IUserRoleTenantRepository
{
    public Task<UserRoleTenant?> GetUserRoleTenantAsync(Guid userId, Guid tenantId, Guid roleId)
    {
        throw new NotImplementedException();
    }
}
