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
            new Permission
            {
                Id = AuthSeedConstants.PermissionIdUserRead,
                Name = PermissionConstants.UserRead,
                Description = "some description",
                CreatedAt = new DateTime(2025, 10, 15, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = new Guid("A0000000-0000-7000-8000-000000000000")
            },
            new Permission
            {
                Id = AuthSeedConstants.PermissionIdPostApprove,
                Name = PermissionConstants.PostApprove,
                CreatedAt = new DateTime(2025, 10, 15, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = new Guid("A0000000-0000-7000-8000-000000000000")
            }
        ]);
    }
}
