using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Stambat.Domain.Entities;

namespace Stambat.Infrastructure.Configurations;

public class TenantProfileConfiguration : IEntityTypeConfiguration<TenantProfile>
{
    public void Configure(EntityTypeBuilder<TenantProfile> builder)
    {
        builder.HasKey(tp => tp.Id);

        // Unique Slug for URLs (e.g., stambat.app/ahmads-coffee)
        builder.Property(tp => tp.Slug)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(tp => tp.Slug)
            .IsUnique();

        // Branding Fields
        builder.Property(tp => tp.LogoUrl)
            .HasMaxLength(500);

        builder.Property(tp => tp.PrimaryColor)
            .HasDefaultValue("#000000")
            .HasMaxLength(7);

        builder.Property(tp => tp.SecondaryColor)
            .HasDefaultValue("#FFFFFF")
            .HasMaxLength(7);
    }
}
