using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Stambat.Domain.Entities;

namespace Stambat.Infrastructure.Configurations;

public class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
{
    public void Configure(EntityTypeBuilder<Invitation> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.TokenHash)
            .IsRequired()
            .HasMaxLength(512);

        builder.HasIndex(i => i.TokenHash)
            .IsUnique();

        builder.HasIndex(i => new { i.Email, i.TenantId, i.RoleId })
            .HasFilter("\"IsUsed\" = false")
            .IsUnique();

        builder.Ignore(i => i.Token);

        builder.Property(i => i.Email).IsRequired()
            .HasMaxLength(256);

        // Relationship to Tenant is optional (Null for new signups)
        builder.HasOne(i => i.Tenant)
            .WithMany()
            .HasForeignKey(i => i.TenantId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
