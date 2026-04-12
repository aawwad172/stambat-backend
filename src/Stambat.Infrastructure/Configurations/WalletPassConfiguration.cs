using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Stambat.Domain.Entities;
using Stambat.Domain.Enums;

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

        builder.Property(wp => wp.Status)
            .IsRequired()
            .HasDefaultValue(WalletPassStatus.Active)
            .HasSentinel((WalletPassStatus)0);

        // Wallet Provider Type
        builder.Property(wp => wp.ProviderType)
            .IsRequired();

        // External Identifiers for Apple/Google Integration
        builder.Property(wp => wp.ApplePassSerialNumber)
            .HasMaxLength(100);

        builder.Property(wp => wp.GooglePayId)
            .HasMaxLength(100);

        // QR Token
        builder.Property(wp => wp.QrTokenPayload)
            .HasMaxLength(500);

        // Redemption
        builder.Property(wp => wp.RedemptionCount)
            .IsRequired()
            .HasDefaultValue(0);

        // Auditing (from IBaseEntity)
        builder.Property(wp => wp.CreatedAt).IsRequired();
        builder.Property(wp => wp.CreatedBy).IsRequired();
        builder.Property(wp => wp.IsDeleted).HasDefaultValue(false);

        // Concurrency token to prevent double-stamping
        builder.Property(wp => wp.CurrentStamps)
            .IsConcurrencyToken();

        // Unique Constraint: One user can only have one active/completed pass per template
        // Redeemed or cancelled passes don't block a new cycle.
        builder.HasIndex(wp => new { wp.UserId, wp.CardTemplateId })
            .IsUnique()
            .HasFilter($"\"IsDeleted\" = false AND \"Status\" IN ({(int)WalletPassStatus.Active}, {(int)WalletPassStatus.Completed})");

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
