using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Stambat.Domain.Entities.Identity;

namespace Stambat.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Key
        builder.HasKey(u => u.Id);

        // Required basics
        builder.OwnsOne(u => u.FullName);

        // Email & Username (unique)
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(u => u.Username).IsUnique();

        // AuthN fields
        builder.Property(u => u.IsActive)
            .HasDefaultValue(true);

        // Auditing
        builder.Property(u => u.CreatedAt).IsRequired();
        builder.Property(u => u.CreatedBy).IsRequired();
        builder.Property(u => u.UpdatedAt).IsRequired(false);
        builder.Property(u => u.UpdatedBy).IsRequired(false);

        // Relationships
        builder.HasMany(u => u.RefreshTokens)
            .WithOne(rt => rt.User)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
