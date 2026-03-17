using Stambat.Domain.Entities.Identity.Authentication;

namespace Stambat.Domain.Interfaces.Infrastructure.IRepositories;

public interface IUserRoleTenantRepository : IRepository<UserRoleTenant>
{
    Task<UserRoleTenant?> GetUserRoleTenantAsync(Guid userId, Guid tenantId, Guid roleId);
}
