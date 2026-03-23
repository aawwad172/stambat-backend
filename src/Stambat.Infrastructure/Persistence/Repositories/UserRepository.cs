using Microsoft.EntityFrameworkCore;

using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Infrastructure.Persistence.Repositories;

public sealed class UserRepository(ApplicationDbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
    public async Task<User?> GetUserByEmailAsync(string email)
        => await _dbSet.IgnoreQueryFilters()
                .Include(user => user.Credentials)
                .Include(user => user.RefreshTokens)
                .Include(user => user.UserTokens)
                .Include(user => user.UserRoleTenants)
                .FirstOrDefaultAsync(user => user.Email == Stambat.Domain.ValueObjects.Email.Create(email));

    public async Task<User?> GetUserByUsernameAsync(string username)
        => await _dbSet
                .Include(user => user.Credentials)
                .Include(user => user.RefreshTokens)
                .Include(user => user.UserTokens)
                .Include(user => user.UserRoleTenants)
                .FirstOrDefaultAsync(user => user.Username == username);

    public async Task<User?> GetByIdWithDetailsAsync(Guid id)
        => await _dbSet
                .Include(user => user.Credentials)
                .Include(user => user.RefreshTokens)
                .Include(user => user.UserTokens)
                .Include(user => user.UserRoleTenants)
                .FirstOrDefaultAsync(user => user.Id == id);
}
