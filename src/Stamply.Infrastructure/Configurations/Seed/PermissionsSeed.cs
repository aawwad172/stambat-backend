using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Stamply.Domain.Entities.Identity.Authentication;
using Stamply.Domain.Enums;

namespace Stamply.Infrastructure.Configurations.Seed;

public class PermissionsSeed : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        // TODO: Add the desired Permissions.
        builder.HasData([
        ]);
    }
}
