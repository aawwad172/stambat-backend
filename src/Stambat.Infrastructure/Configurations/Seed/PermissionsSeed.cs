using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Enums;

namespace Stambat.Infrastructure.Configurations.Seed;

public class PermissionsSeed : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasData([
            // Tenants
            CreatePermission(AuthSeedConstants.PermissionIdTenantsView, PermissionConstants.TenantsView),
            CreatePermission(AuthSeedConstants.PermissionIdTenantsAdd, PermissionConstants.TenantsAdd),
            CreatePermission(AuthSeedConstants.PermissionIdTenantsEdit, PermissionConstants.TenantsEdit),
            CreatePermission(AuthSeedConstants.PermissionIdTenantsDelete, PermissionConstants.TenantsDelete),
            CreatePermission(AuthSeedConstants.PermissionIdTenantsSetup, PermissionConstants.TenantsSetup),

            // Users
            CreatePermission(AuthSeedConstants.PermissionIdUsersView, PermissionConstants.UsersView),
            CreatePermission(AuthSeedConstants.PermissionIdUsersAdd, PermissionConstants.UsersAdd),
            CreatePermission(AuthSeedConstants.PermissionIdUsersEdit, PermissionConstants.UsersEdit),
            CreatePermission(AuthSeedConstants.PermissionIdUsersDelete, PermissionConstants.UsersDelete),

            // Invitations
            CreatePermission(AuthSeedConstants.PermissionIdInvitationsView, PermissionConstants.InvitationsView),
            CreatePermission(AuthSeedConstants.PermissionIdInvitationsAdd, PermissionConstants.InvitationsAdd),
            CreatePermission(AuthSeedConstants.PermissionIdInvitationsEdit, PermissionConstants.InvitationsEdit),
            CreatePermission(AuthSeedConstants.PermissionIdInvitationsDelete, PermissionConstants.InvitationsDelete),

            // Cards
            CreatePermission(AuthSeedConstants.PermissionIdCardsView, PermissionConstants.CardsView),
            CreatePermission(AuthSeedConstants.PermissionIdCardsAdd, PermissionConstants.CardsAdd),
            CreatePermission(AuthSeedConstants.PermissionIdCardsEdit, PermissionConstants.CardsEdit),
            CreatePermission(AuthSeedConstants.PermissionIdCardsDelete, PermissionConstants.CardsDelete),

            // Rewards
            CreatePermission(AuthSeedConstants.PermissionIdRewardsView, PermissionConstants.RewardsView),
            CreatePermission(AuthSeedConstants.PermissionIdRewardsAdd, PermissionConstants.RewardsAdd),
            CreatePermission(AuthSeedConstants.PermissionIdRewardsEdit, PermissionConstants.RewardsEdit),
            CreatePermission(AuthSeedConstants.PermissionIdRewardsDelete, PermissionConstants.RewardsDelete),

            // Scan
            CreatePermission(AuthSeedConstants.PermissionIdScanStamping, PermissionConstants.ScanStamping),
            CreatePermission(AuthSeedConstants.PermissionIdScanRedeem, PermissionConstants.ScanRedeem),

            // Super Admin
            CreatePermission(AuthSeedConstants.PermissionIdSystemManage, PermissionConstants.SystemManage),
            CreatePermission(AuthSeedConstants.PermissionIdSystemLogsView, PermissionConstants.SystemLogsView),
            CreatePermission(AuthSeedConstants.PermissionIdSystemAuditView, PermissionConstants.SystemAuditView),
            CreatePermission(AuthSeedConstants.PermissionIdSystemSettingsEdit, PermissionConstants.SystemSettingsEdit),
            CreatePermission(AuthSeedConstants.PermissionIdTenantsManage, PermissionConstants.TenantsManage)
        ]);
    }

    private static Permission CreatePermission(Guid id, string name)
    {
        var p = Permission.Create(name, id: id);
        p.CreatedAt = AuthSeedConstants.SeedDateUtc;
        p.CreatedBy = AuthSeedConstants.SystemUserId;
        return p;
    }
}
