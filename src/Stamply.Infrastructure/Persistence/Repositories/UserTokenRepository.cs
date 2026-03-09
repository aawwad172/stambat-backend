using System;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Stamply.Domain.Entities;
using Stamply.Domain.Entities.Identity;
using Stamply.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stamply.Infrastructure.Persistence.Repositories;

public class UserTokenRepository(ApplicationDbContext dbContext)
: Repository<UserToken>(dbContext), IUserTokenRepository
{
    public async Task<UserToken?> GetUserTokenByTokenAsync(string token)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Token == token);
    }
}
