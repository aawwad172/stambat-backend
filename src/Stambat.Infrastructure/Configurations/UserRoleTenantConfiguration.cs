using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Stambat.Domain.Entities.Identity.Authentication;

namespace Stambat.Infrastructure.Configurations;

public class UserRoleTenantConfiguration : IEntityTypeConfiguration<UserRoleTenant>
{
    public void Configure(EntityTypeBuilder<UserRoleTenant> builder)
    {
        builder.HasKey(urt => urt.Id); // Use the surrogate ID

        builder.HasOne(urt => urt.User)
            .WithMany(u => u.UserRoleTenants)
            .HasForeignKey(urt => urt.UserId);

        builder.HasOne(urt => urt.Role)
            .WithMany(urt => urt.UserRoleTenants)
            .HasForeignKey(urt => urt.RoleId);

        builder.HasOne(urt => urt.Tenant)
            .WithMany(t => t.UserRoleTenants)
            .HasForeignKey(urt => urt.TenantId)
            .OnDelete(DeleteBehavior.Cascade);

        // Unique constraint for tenant-specific roles (excludes super admins)
        builder.HasIndex(urt => new { urt.UserId, urt.RoleId, urt.TenantId })
            .IsUnique()
            .HasFilter("\"TenantId\" IS NOT NULL");  // Only for tenant users

        // Separate unique constraint for super admin roles (TenantId = NULL)
        builder.HasIndex(urt => new { urt.UserId, urt.RoleId })
            .IsUnique()
            .HasFilter("\"TenantId\" IS NULL");  // Only for super admins
    }
}
