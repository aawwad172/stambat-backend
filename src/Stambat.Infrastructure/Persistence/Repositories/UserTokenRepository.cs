using Microsoft.EntityFrameworkCore;

using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Infrastructure.Persistence.Repositories;

public class UserTokenRepository(ApplicationDbContext dbContext)
: Repository<UserToken>(dbContext), IUserTokenRepository
{
    public async Task<UserToken?> GetUserTokenByTokenAsync(string token)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Token == token);
    }
}
