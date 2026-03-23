using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Stambat.Domain.Entities.Identity.Authentication;

namespace Stambat.Infrastructure.Configurations.Seed;

public class RolesSeed : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        var superAdmin = Role.Create("SuperAdmin", "Full system administration and management.", AuthSeedConstants.RoleIdSuperAdmin);
        superAdmin.CreatedAt = AuthSeedConstants.SeedDateUtc;
        superAdmin.CreatedBy = AuthSeedConstants.SystemUserId;

        var tenantAdmin = Role.Create("TenantAdmin", "Administration of a specific tenant and its resources.", AuthSeedConstants.RoleIdAdmin);
        tenantAdmin.CreatedAt = AuthSeedConstants.SeedDateUtc;
        tenantAdmin.CreatedBy = AuthSeedConstants.SystemUserId;

        var merchant = Role.Create("Merchant", "Staff access for scanning and stamping loyalty cards.", AuthSeedConstants.RoleIdMerchant);
        merchant.CreatedAt = AuthSeedConstants.SeedDateUtc;
        merchant.CreatedBy = AuthSeedConstants.SystemUserId;

        var manager = Role.Create("Manager", "Middle management for specific tenant features and staff management.", AuthSeedConstants.RoleIdManager);
        manager.CreatedAt = AuthSeedConstants.SeedDateUtc;
        manager.CreatedBy = AuthSeedConstants.SystemUserId;

        var userRole = Role.Create("User", "End-user access for viewing cards and rewards.", AuthSeedConstants.RoleIdUser);
        userRole.CreatedAt = AuthSeedConstants.SeedDateUtc;
        userRole.CreatedBy = AuthSeedConstants.SystemUserId;

        builder.HasData([superAdmin, tenantAdmin, merchant, manager, userRole]);
    }
}
