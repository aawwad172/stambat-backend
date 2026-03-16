using Microsoft.EntityFrameworkCore;

using Stamply.Domain.Entities;
using Stamply.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stamply.Infrastructure.Persistence.Repositories;

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
}
