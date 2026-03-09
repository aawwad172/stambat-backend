using Stamply.Domain.Entities;

namespace Stamply.Domain.Interfaces.Infrastructure.IRepositories;

public interface ITenantRepository : IRepository<Tenant>
{
    Task<Tenant?> GetTenantByEmailAsync(string email);
}
