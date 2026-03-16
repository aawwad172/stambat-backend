using Stamply.Domain.Entities.Identity.Authentication;

namespace Stamply.Domain.Interfaces.Infrastructure.IRepositories;

public interface IUserRoleTenantRepository : IRepository<UserRoleTenant>
{
    Task<UserRoleTenant?> GetUserRoleTenantAsync(Guid userId, Guid tenantId, Guid roleId);
}
