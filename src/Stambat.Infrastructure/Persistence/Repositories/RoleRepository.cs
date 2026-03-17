using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

using Microsoft.EntityFrameworkCore;
using Stambat.Domain.Entities.Identity.Authentication;

namespace Stambat.Infrastructure.Persistence.Repositories;

public class RoleRepository(ApplicationDbContext context) : Repository<Role>(context), IRoleRepository
{
    public async Task<Role?> GetRoleByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Name == name);
    }
}
