using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Stambat.Domain.Entities.Identity;

namespace Stambat.Infrastructure.Configurations;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.HasKey(ut => ut.Id);

        builder.Property(ut => ut.Token)
               .IsRequired()
               .HasMaxLength(256);

        builder.HasIndex(ut => ut.Token)
            .IsUnique();

        builder.HasOne(ut => ut.User)
              .WithMany()
              .HasForeignKey(ut => ut.UserId);
    }
}
