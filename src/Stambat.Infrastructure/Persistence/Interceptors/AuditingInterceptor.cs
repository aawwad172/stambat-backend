using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Domain.Auditing;
using Stambat.Infrastructure.Configurations.Seed;

namespace Stambat.Infrastructure.Persistence.Interceptors;

public class AuditingInterceptor(ICurrentUserService currentUserService) : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateAuditFields(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateAuditFields(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateAuditFields(DbContext? context)
    {
        if (context is null) return;

        Guid userId = _currentUserService.UserId != Guid.Empty
            ? _currentUserService.UserId
            : AuthSeedConstants.SystemUserId;

        DateTime now = DateTime.UtcNow;

        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added)
            {
                if (entry.Entity is ICreationAudit creationAudit)
                {
                    if (creationAudit.CreatedAt == default) creationAudit.CreatedAt = now;
                    if (creationAudit.CreatedBy == Guid.Empty) creationAudit.CreatedBy = userId;
                }
            }
            else if (entry.State == EntityState.Modified)
            {
                if (entry.Entity is IModificationAudit modificationAudit)
                {
                    modificationAudit.UpdatedAt = now;
                    modificationAudit.UpdatedBy = userId;
                }
            }
        }
    }
}
