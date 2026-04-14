using Microsoft.EntityFrameworkCore;

using Stambat.Domain.Entities;
using Stambat.Domain.Enums;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;
using Stambat.Infrastructure.Pagination;

namespace Stambat.Infrastructure.Persistence.Repositories;

public class InvitationRepository(ApplicationDbContext context)
: Repository<Invitation>(context), IInvitationRepository
{
    public async Task<Invitation?> GetInvitationByTokenHashAsync(string tokenHash)
    {
        return await _dbSet
            .Include(i => i.Role)
            .Include(i => i.Tenant)
            .FirstOrDefaultAsync(i => i.TokenHash == tokenHash);
    }

    public async Task<Invitation?> GetLastActiveInvitationForTenantAndRole(string email, Guid tenantId, Guid roleId)
    {
        return await _dbSet
            .Where(i => i.IsUsed == false
                        && i.ExpiresAt >= DateTime.UtcNow
                        && i.Email == email
                        && i.TenantId == tenantId
                        && i.RoleId == roleId)
            .OrderByDescending(i => i.CreatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> ExistsActiveAsync(string email, Guid tenantId, Guid roleId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(i =>
            i.IsUsed == false &&
            i.Email == email &&
            i.TenantId == tenantId &&
            i.IsCancelled == false &&
            i.RoleId == roleId, cancellationToken);
    }

    public async Task<PaginationResult<Invitation>> GetTenantInvitationsAsync(
        Guid tenantId, int page, int size,
        InvitationStatus? status, Guid? roleId,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Invitation> query = _dbSet
            .AsNoTracking()
            .Include(i => i.Role)
            .Where(i => i.TenantId == tenantId && !i.IsUsed && !i.IsDeleted);

        if (roleId.HasValue)
            query = query.Where(i => i.RoleId == roleId.Value);

        query = status switch
        {
            InvitationStatus.Pending => query.Where(i => !i.IsCancelled && i.ExpiresAt >= DateTime.UtcNow),
            InvitationStatus.Expired => query.Where(i => !i.IsCancelled && i.ExpiresAt < DateTime.UtcNow),
            InvitationStatus.Cancelled => query.Where(i => i.IsCancelled),
            _ => query
        };

        return await query
            .OrderByDescending(i => i.CreatedAt)
            .ToPagedQueryAsync(page, size);
    }

    public async Task<Invitation?> GetByIdForTenantAsync(Guid invitationId, Guid tenantId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(i => i.Id == invitationId && i.TenantId == tenantId);
    }
}
