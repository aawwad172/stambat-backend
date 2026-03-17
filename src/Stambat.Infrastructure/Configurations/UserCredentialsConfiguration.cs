using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Stambat.Domain.Entities.Identity;

namespace Stambat.Infrastructure.Configurations;

public class UserCredentialsConfiguration : IEntityTypeConfiguration<UserCredentials>
{
    public void Configure(EntityTypeBuilder<UserCredentials> builder)
    {
        // Key
        builder.HasKey(c => c.Id);

        // Required basics
        builder.Property(c => c.PasswordHash)
            .IsRequired()
            .HasMaxLength(256);

        // Relationships
        builder.HasOne(c => c.User)
            .WithOne(u => u.Credentials)
            .HasForeignKey<UserCredentials>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
