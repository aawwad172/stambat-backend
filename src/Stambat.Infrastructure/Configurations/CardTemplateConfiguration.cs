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

        // Ensure a shop doesn't have two templates with the exact same name
        builder.HasIndex(ct => new { ct.TenantId, ct.Title }).IsUnique();

        builder.HasOne(ct => ct.Tenant)
            .WithMany(t => t.CardTemplates)
            .HasForeignKey(ct => ct.TenantId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
