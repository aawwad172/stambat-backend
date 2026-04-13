using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Stambat.Domain.Entities;
using Stambat.Domain.Enums;

namespace Stambat.Infrastructure.Configurations;

public class CardTemplateConfiguration : IEntityTypeConfiguration<CardTemplate>
{
    public void Configure(EntityTypeBuilder<CardTemplate> builder)
    {
        builder.HasKey(ct => ct.Id);

        builder.Property(ct => ct.Title).IsRequired().HasMaxLength(100);
        builder.Property(ct => ct.Description).HasMaxLength(500);
        builder.Property(ct => ct.RewardDescription).HasMaxLength(200);
        builder.Property(ct => ct.TermsAndConditions).HasMaxLength(2000);
        builder.Property(ct => ct.PrimaryColorOverride).HasMaxLength(500);
        builder.Property(ct => ct.SecondaryColorOverride).HasMaxLength(500);
        builder.Property(ct => ct.LogoUrlOverride).HasMaxLength(500);
        builder.Property(ct => ct.EmptyStampUrl).HasMaxLength(500);
        builder.Property(ct => ct.EarnedStampUrl).HasMaxLength(500);
        builder.Property(ct => ct.JoinUrl).HasMaxLength(500);
        builder.Property(ct => ct.JoinQrCodeBase64).HasColumnType("text");
        builder.Property(ct => ct.WalletClassId).HasMaxLength(200);

        // Balance and rules
        builder.Property(ct => ct.RequiredBalance)
            .IsRequired()
            .HasColumnType("numeric");

        // Card type and expiry
        builder.Property(ct => ct.CardType)
            .IsRequired()
            .HasDefaultValue(CardType.Standard)
            .HasSentinel((CardType)0);

        builder.Property(ct => ct.ExpiryDurationInDays)
            .IsRequired(false);

        // Redemption type and conversion rate
        builder.Property(ct => ct.RedemptionType)
            .IsRequired()
            .HasDefaultValue(RedemptionType.Stamps)
            .HasSentinel((RedemptionType)0);

        builder.Property(ct => ct.PointsPerCurrencyUnit)
            .IsRequired(false)
            .HasColumnType("numeric");

        // Ensure a shop doesn't have two templates with the exact same name (soft-deleted rows excluded)
        builder.HasIndex(ct => new { ct.TenantId, ct.Title })
            .IsUnique()
            .HasFilter("\"IsDeleted\" = false");

        builder.HasOne(ct => ct.Tenant)
            .WithMany(t => t.CardTemplates)
            .HasForeignKey(ct => ct.TenantId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
