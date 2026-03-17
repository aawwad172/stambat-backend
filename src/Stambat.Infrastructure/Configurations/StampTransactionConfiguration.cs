using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Stambat.Domain.Entities;

namespace Stambat.Infrastructure.Configurations;

public class StampTransactionConfiguration : IEntityTypeConfiguration<StampTransaction>
{
    public void Configure(EntityTypeBuilder<StampTransaction> builder)
    {
        // Primary Key
        builder.HasKey(st => st.Id);

        // Data fields
        builder.Property(st => st.StampsAdded)
            .IsRequired()
            .HasDefaultValue(1);

        builder.Property(st => st.Note)
            .HasMaxLength(250);

        // Auditing (from IBaseEntity)
        builder.Property(st => st.CreatedAt).IsRequired();
        builder.Property(st => st.CreatedBy).IsRequired();
        builder.Property(st => st.IsDeleted).HasDefaultValue(false);

        // Relationships

        // 1. Link to the specific Wallet Pass
        builder.HasOne(st => st.WalletPass)
            .WithMany(wp => wp.Transactions) // Make sure to add this collection to WalletPass.cs
            .HasForeignKey(st => st.WalletPassId)
            .OnDelete(DeleteBehavior.Cascade);

        // 2. Link to the Merchant (Staff Member)
        builder.HasOne(st => st.Merchant)
            .WithMany()
            .HasForeignKey(st => st.MerchantId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
