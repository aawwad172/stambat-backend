using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Stambat.Domain.Entities;

namespace Stambat.Infrastructure.Configurations;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.HasKey(t => t.Id);
        // Business Name
        builder.Property(t => t.BusinessName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Email)
            .IsRequired();

        builder.HasIndex(t => t.Email)
            .IsUnique();

        // Regional Settings
        builder.Property(t => t.TimeZoneId)
            .HasDefaultValue("Asia/Amman")
            .HasMaxLength(50);

        builder.Property(t => t.CurrencyCode)
            .HasDefaultValue("JOD")
            .HasMaxLength(3);

        // Status
        builder.Property(t => t.IsActive)
            .HasDefaultValue(true);

        // Auditing
        builder.Property(t => t.CreatedAt).IsRequired();
        builder.Property(t => t.CreatedBy).IsRequired();
        builder.Property(t => t.UpdatedAt).IsRequired(false);
        builder.Property(t => t.UpdatedBy).IsRequired(false);
        builder.Property(t => t.IsDeleted).HasDefaultValue(false);

        // Relationships
        // One to one relationship with the profiel
        builder.HasOne(t => t.TenantProfile)
            .WithOne(tp => tp.Tenant)
            .HasForeignKey<TenantProfile>(tp => tp.TenantId);

        builder.HasMany(t => t.UserRoleTenants)
            .WithOne(urt => urt.Tenant)
            .HasForeignKey(urt => urt.TenantId)
            .OnDelete(DeleteBehavior.Cascade);

        // The many-to-many relationship is already configured in UserRoleTenantConfiguration,
        // but we define the CardTemplate one-to-many here.
        // builder.HasMany(t => t.CardTemplates)
        //     .WithOne(ct => ct.Tenant)
        //     .HasForeignKey(ct => ct.TenantId)
        //     .OnDelete(DeleteBehavior.Restrict);
        // Restrict prevents deleting a Tenant if they have active templates
    }
}
