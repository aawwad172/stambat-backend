using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Stambat.Domain.Entities;

namespace Stambat.Infrastructure.Configurations;

public class WalletPassConfiguration : IEntityTypeConfiguration<WalletPass>
{
    public void Configure(EntityTypeBuilder<WalletPass> builder)
    {
        // Primary Key
        builder.HasKey(wp => wp.Id);

        // Data fields
        builder.Property(wp => wp.CurrentStamps)
            .IsRequired()
            .HasDefaultValue(0);

        // External Identifiers for Apple/Google Integration
        builder.Property(wp => wp.ApplePassSerialNumber)
            .HasMaxLength(100);

        builder.Property(wp => wp.GooglePayId)
            .HasMaxLength(100);

        // Unique Constraint: One user can only have one pass per template
        // This prevents double-claiming the same loyalty card.
        builder.HasIndex(wp => new { wp.UserId, wp.CardTemplateId })
            .IsUnique();

        // 1. Link to the User (The Customer)
        builder.HasOne(wp => wp.User)
            .WithMany(u => u.WalletPasses)
            .HasForeignKey(wp => wp.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // 2. Link to the Template (The Rules)
        builder.HasOne(wp => wp.CardTemplate)
            .WithMany(ct => ct.IssuedPasses)
            .HasForeignKey(wp => wp.CardTemplateId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
