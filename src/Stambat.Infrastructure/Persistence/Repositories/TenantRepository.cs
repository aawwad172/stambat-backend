using Microsoft.EntityFrameworkCore;

using Stambat.Domain.Entities;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Infrastructure.Persistence.Repositories;

public class TenantRepository(ApplicationDbContext dbContext) : Repository<Tenant>(dbContext), ITenantRepository
{
    public async Task<Tenant?> GetTenantByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(t => t.Email == email);
    }
}
