using Stambat.Domain.Entities;

namespace Stambat.Domain.Interfaces.Infrastructure.IRepositories;

public interface ITenantRepository : IRepository<Tenant>
{
    Task<Tenant?> GetTenantByEmailAsync(string email);
}
