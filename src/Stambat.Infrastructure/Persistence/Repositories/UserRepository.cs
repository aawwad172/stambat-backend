using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

using Microsoft.EntityFrameworkCore;
using Stambat.Domain.Entities.Identity;

namespace Stambat.Infrastructure.Persistence.Repositories;

public sealed class UserRepository(ApplicationDbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
    public async Task<User?> GetUserByEmailAsync(string email)
        => await _dbSet.IgnoreQueryFilters()
                .Include(user => user.Credentials)
                .FirstOrDefaultAsync(user => user.Email == email);

    public async Task<User?> GetUserByUsernameAsync(string username)
        => await _dbSet.Include(user => user.Credentials)
                .FirstOrDefaultAsync(user => user.Username == username);
}
