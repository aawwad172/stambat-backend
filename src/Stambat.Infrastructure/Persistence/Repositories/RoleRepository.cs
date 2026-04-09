using Microsoft.EntityFrameworkCore;

using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Enums;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Infrastructure.Persistence.Repositories;

public class RoleRepository(ApplicationDbContext context) : Repository<Role>(context), IRoleRepository
{
    public async Task<Role?> GetRoleByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<IEnumerable<Role>> GetStaffRolesAsync()
    {
        return await _dbSet
            .AsNoTracking()
            .Where(r => r.Name == nameof(RolesEnum.Manager) || r.Name == nameof(RolesEnum.Merchant))
            .ToListAsync();
    }
}
