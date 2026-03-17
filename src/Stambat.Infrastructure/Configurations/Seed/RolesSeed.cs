using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Stambat.Domain.Entities.Identity.Authentication;

namespace Stambat.Infrastructure.Configurations.Seed;

public class RolesSeed : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData([
             new Role
             {
                 Id = AuthSeedConstants.RoleIdSuperAdmin,
                 Name = "SuperAdmin",
                 Description = "Full system administration and management.",
                 CreatedAt = AuthSeedConstants.SeedDateUtc,
                 CreatedBy = AuthSeedConstants.SystemUserId
             },
            new Role
            {
                Id = AuthSeedConstants.RoleIdAdmin,
                Name = "TenantAdmin",
                Description = "Administration of a specific tenant and its resources.",
                CreatedAt = AuthSeedConstants.SeedDateUtc,
                CreatedBy = AuthSeedConstants.SystemUserId
            },
            new Role
            {
                Id = AuthSeedConstants.RoleIdMerchant,
                Name = "Merchant",
                Description = "Staff access for scanning and stamping loyalty cards.",
                CreatedAt = AuthSeedConstants.SeedDateUtc,
                CreatedBy = AuthSeedConstants.SystemUserId
            },
            new Role
            {
                Id = AuthSeedConstants.RoleIdManager,
                Name = "Manager",
                Description = "Middle management for specific tenant features and staff management.",
                CreatedAt = AuthSeedConstants.SeedDateUtc,
                CreatedBy = AuthSeedConstants.SystemUserId
            },
            new Role
            {
                Id = AuthSeedConstants.RoleIdUser,
                Name = "User",
                Description = "End-user access for viewing cards and rewards.",
                CreatedAt = AuthSeedConstants.SeedDateUtc,
                CreatedBy = AuthSeedConstants.SystemUserId
            }
        ]);
    }
}
