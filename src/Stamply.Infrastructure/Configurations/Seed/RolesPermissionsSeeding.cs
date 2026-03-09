using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Stamply.Domain.Entities.Identity.Authentication;

namespace Stamply.Infrastructure.Configurations.Seed;

public class RolesPermissionsSeed : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasData([
        ]);
    }
}
