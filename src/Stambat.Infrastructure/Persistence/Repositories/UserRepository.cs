using Microsoft.EntityFrameworkCore;

using Stambat.Domain.Entities;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Enums;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;
using Stambat.Infrastructure.Pagination;

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

    public async Task<User?> GetUserByIdentityTokenAsync(string token)
        => await _dbSet
                .Include(user => user.Credentials)
                .Include(user => user.RefreshTokens)
                .Include(user => user.UserTokens)
                .Include(user => user.UserRoleTenants)
                .FirstOrDefaultAsync(user => user.UserTokens.Any(ut =>
                    ut.Type == UserTokenType.TenantSelection &&
                    ut.Token == token &&
                    !ut.IsUsed &&
                    ut.ExpiryDate > DateTime.UtcNow));

    public async Task<PaginationResult<User>> GetStaffByTenantAsync(
        Guid tenantId,
        int? pageNumber,
        int? pageSize)
    {
        IQueryable<User> query = _dbSet
            .AsNoTracking()
            .Include(u => u.UserRoleTenants.Where(
                urt => urt.TenantId == tenantId && urt.IsActive
                ))
                .ThenInclude(urt => urt.Role)
                    .Where(u => u.IsActive
                            && u.UserRoleTenants.Any(urt => urt.TenantId == tenantId && urt.IsActive));

        return await query.ToPagedQueryAsync(pageNumber, pageSize);
    }

    public async Task<User?> GetStaffMemberByTenantAsync(Guid tenantId, Guid staffId)
        => await _dbSet
            .Include(u => u.UserRoleTenants.Where(urt => urt.TenantId == tenantId))
                .ThenInclude(urt => urt.Role)
            .FirstOrDefaultAsync(u => u.Id == staffId
                && u.UserRoleTenants.Any(urt => urt.TenantId == tenantId));
}

