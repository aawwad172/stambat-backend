using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Stambat.Domain.Entities;

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

        // Ensure a shop doesn't have two templates with the exact same name
        builder.HasIndex(ct => new { ct.TenantId, ct.Title }).IsUnique();

        builder.HasOne(ct => ct.Tenant)
            .WithMany(t => t.CardTemplates)
            .HasForeignKey(ct => ct.TenantId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
