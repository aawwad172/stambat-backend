using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

using Stambat.Domain.Entities;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Domain;
using Stambat.Domain.Interfaces.Domain.Auditing;
using Stambat.Infrastructure.Configurations;
using Stambat.Infrastructure.Configurations.Seed;

namespace Stambat.Infrastructure.Persistence;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    ILogger<ApplicationDbContext> logger,
    ICurrentUserService currentUserService)
    : DbContext(options)
{
    private readonly ILogger<ApplicationDbContext> _logger = logger;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    // DbSet properties for the main entities and join tables
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserCredentials> UserCredentials { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Permission> Permissions { get; set; } = null!;
    public DbSet<UserRoleTenant> UserRoleTenants { get; set; } = null!;
    public DbSet<RolePermission> RolePermissions { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

    public DbSet<Tenant> Tenants { get; set; } = null!;
    public DbSet<TenantProfile> TenantProfiles { get; set; } = null!;
    public DbSet<UserToken> UserTokens { get; set; } = null!;
    public DbSet<Invitation> Invitations { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ApplySoftDeleteFilters(modelBuilder); // Cleaner call in OnModelCreating

        // Apply configurations in specific order
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserCredentialsConfiguration());

        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new RolesSeed());

        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionsSeed());

        // Apply relationship configurations last
        modelBuilder.ApplyConfiguration(new RolesPermissionsSeed());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleTenantConfiguration());

        modelBuilder.ApplyConfiguration(new TenantConfiguration());
        modelBuilder.ApplyConfiguration(new InvitationConfiguration());
        modelBuilder.ApplyConfiguration(new TenantProfileConfiguration());
        modelBuilder.ApplyConfiguration(new UserTokenConfiguration());

        // todo: refactor this to be more clean (add it into a method)
        // In ApplicationDbContext.OnModelCreating, after base.OnModelCreating:
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            IMutableProperty? idProperty = entityType.FindProperty(nameof(IEntity.Id));
            if (idProperty != null && idProperty.ClrType == typeof(Guid))
            {
                idProperty.ValueGenerated = ValueGenerated.Never;
            }
        }
    }

    // _logger service
    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        // Log database changes before saving
        LogChanges();

        var utcNow = DateTime.UtcNow;
        var currentUserId = _currentUserService.UserId;

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added && entry.Entity is ICreationAudit creationAudit)
            {
                if (creationAudit.CreatedAt == default)
                    creationAudit.CreatedAt = utcNow;

                if (creationAudit.CreatedBy == Guid.Empty && currentUserId != Guid.Empty)
                    creationAudit.CreatedBy = currentUserId;
            }

            // Optional: If you have an interface for updates (e.g., IUpdateAudit)
            if (entry.State == EntityState.Modified && entry.Entity is IModificationAudit updateAudit)
            {
                updateAudit.UpdatedAt = utcNow;
                updateAudit.UpdatedBy = currentUserId;
            }
        }

        int result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

        // Log successful save
        _logger.LogInformation("Database changes successfully saved. Affected rows: {Result}", result);

        return result;
    }

    private void LogChanges()
    {
        foreach (EntityEntry entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added)
            {
                _logger.LogInformation($"Adding new entity: {entry.Entity.GetType().Name} - Values: {entry.CurrentValues.ToObject()}");
            }
            else if (entry.State == EntityState.Modified)
            {
                _logger.LogInformation($"Updating entity: {entry.Entity.GetType().Name} - Old Values: {entry.OriginalValues.ToObject()} - New Values: {entry.CurrentValues.ToObject()}");
            }
            else if (entry.State == EntityState.Deleted)
            {
                _logger.LogInformation($"Deleting entity: {entry.Entity.GetType().Name} - Values: {entry.OriginalValues.ToObject()}");
            }
        }
    }

    /// <summary>
    /// Applies the global query filter for soft deletion to all entities 
    /// that implement the ISoftDelete interface.
    /// </summary>
    /// <param name="modelBuilder">The model builder instance.</param>
    private void ApplySoftDeleteFilters(ModelBuilder modelBuilder)
    {
        // Find all entity types that implement ISoftDelete
        IEnumerable<IMutableEntityType> softDeleteEntityTypes = modelBuilder.Model.GetEntityTypes()
            .Where(e => typeof(ISoftDelete).IsAssignableFrom(e.ClrType));

        // Apply the filter for each found entity
        foreach (IMutableEntityType? entityType in softDeleteEntityTypes)
        {
            // Use a helper method to create and apply the non-generic filter expression
            MethodInfo method = typeof(ApplicationDbContext)
                .GetMethod(nameof(SetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)!
                .MakeGenericMethod(entityType.ClrType);

            method.Invoke(null, new object[] { modelBuilder });
        }
    }

    // A generic static method to create and apply the filter.
    // Making it static avoids potential closure capture issues with the DbContext instance.
    private static void SetSoftDeleteFilter<TEntity>(ModelBuilder modelBuilder) where TEntity : class, ISoftDelete
    {
        // The filter expression: entity.IsDeleted == false
        modelBuilder.Entity<TEntity>().HasQueryFilter(
            e => !EF.Property<bool>(e, nameof(ISoftDelete.IsDeleted)));
    }
}
