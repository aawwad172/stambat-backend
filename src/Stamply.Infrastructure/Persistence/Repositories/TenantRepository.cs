using Microsoft.EntityFrameworkCore;

using Stamply.Domain.Entities;
using Stamply.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stamply.Infrastructure.Persistence.Repositories;

public class TenantRepository(ApplicationDbContext dbContext) : Repository<Tenant>(dbContext), ITenantRepository
{
    public async Task<Tenant?> GetTenantByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(t => t.Email == email);
    }
}
