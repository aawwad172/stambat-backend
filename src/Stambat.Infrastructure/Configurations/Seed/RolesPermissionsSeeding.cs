using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Stambat.Domain.Entities.Identity.Authentication;

namespace Stambat.Infrastructure.Configurations.Seed;

public class RolesPermissionsSeed : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasData([
            // SuperAdmin (All 27)
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdTenantsView),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdTenantsAdd),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdTenantsEdit),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdTenantsDelete),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdTenantsSetup),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdUsersView),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdUsersAdd),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdUsersEdit),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdUsersDelete),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdInvitationsView),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdInvitationsAdd),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdInvitationsEdit),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdInvitationsDelete),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdCardsView),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdCardsAdd),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdCardsEdit),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdCardsDelete),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdRewardsView),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdRewardsAdd),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdRewardsEdit),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdRewardsDelete),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdScanStamping),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdScanRedeem),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdSystemManage),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdSystemLogsView),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdSystemAuditView),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdSystemSettingsEdit),
            RolePermission.Create(AuthSeedConstants.RoleIdSuperAdmin, AuthSeedConstants.PermissionIdTenantsManage),

            // TenantAdmin (20)
            RolePermission.Create(AuthSeedConstants.RoleIdAdmin, AuthSeedConstants.PermissionIdTenantsView),
            RolePermission.Create(AuthSeedConstants.RoleIdAdmin, AuthSeedConstants.PermissionIdTenantsEdit),
            RolePermission.Create(AuthSeedConstants.RoleIdAdmin, AuthSeedConstants.PermissionIdUsersView),
            RolePermission.Create(AuthSeedConstants.RoleIdAdmin, AuthSeedConstants.PermissionIdUsersAdd),
            RolePermission.Create(AuthSeedConstants.RoleIdAdmin, AuthSeedConstants.PermissionIdUsersEdit),
            RolePermission.Create(AuthSeedConstants.RoleIdAdmin, AuthSeedConstants.PermissionIdUsersDelete),
            RolePermission.Create(AuthSeedConstants.RoleIdAdmin, AuthSeedConstants.PermissionIdInvitationsView),
            RolePermission.Create(AuthSeedConstants.RoleIdAdmin, AuthSeedConstants.PermissionIdInvitationsAdd),
            RolePermission.Create(AuthSeedConstants.RoleIdAdmin, AuthSeedConstants.PermissionIdInvitationsEdit),
            RolePermission.Create(AuthSeedConstants.RoleIdAdmin, AuthSeedConstants.PermissionIdInvitationsDelete),
            RolePermission.Create(AuthSeedConstants.RoleIdAdmin, AuthSeedConstants.PermissionIdCardsView),
            RolePermission.Create(AuthSeedConstants.RoleIdAdmin, AuthSeedConstants.PermissionIdCardsAdd),
            RolePermission.Create(AuthSeedConstants.RoleIdAdmin, AuthSeedConstants.PermissionIdCardsEdit),
            RolePermission.Create(AuthSeedConstants.RoleIdAdmin, AuthSeedConstants.PermissionIdCardsDelete),
            RolePermission.Create(AuthSeedConstants.RoleIdAdmin, AuthSeedConstants.PermissionIdRewardsView),
            RolePermission.Create(AuthSeedConstants.RoleIdAdmin, AuthSeedConstants.PermissionIdRewardsAdd),
            RolePermission.Create(AuthSeedConstants.RoleIdAdmin, AuthSeedConstants.PermissionIdRewardsEdit),
            RolePermission.Create(AuthSeedConstants.RoleIdAdmin, AuthSeedConstants.PermissionIdRewardsDelete),
            RolePermission.Create(AuthSeedConstants.RoleIdAdmin, AuthSeedConstants.PermissionIdScanStamping),
            RolePermission.Create(AuthSeedConstants.RoleIdAdmin, AuthSeedConstants.PermissionIdScanRedeem),

            // Merchant (6)
            RolePermission.Create(AuthSeedConstants.RoleIdMerchant, AuthSeedConstants.PermissionIdTenantsView),
            RolePermission.Create(AuthSeedConstants.RoleIdMerchant, AuthSeedConstants.PermissionIdUsersView),
            RolePermission.Create(AuthSeedConstants.RoleIdMerchant, AuthSeedConstants.PermissionIdCardsView),
            RolePermission.Create(AuthSeedConstants.RoleIdMerchant, AuthSeedConstants.PermissionIdRewardsView),
            RolePermission.Create(AuthSeedConstants.RoleIdMerchant, AuthSeedConstants.PermissionIdScanStamping),
            RolePermission.Create(AuthSeedConstants.RoleIdMerchant, AuthSeedConstants.PermissionIdScanRedeem),

            // Manager (10)
            RolePermission.Create(AuthSeedConstants.RoleIdManager, AuthSeedConstants.PermissionIdTenantsView),
            RolePermission.Create(AuthSeedConstants.RoleIdManager, AuthSeedConstants.PermissionIdUsersView),
            RolePermission.Create(AuthSeedConstants.RoleIdManager, AuthSeedConstants.PermissionIdUsersAdd),
            RolePermission.Create(AuthSeedConstants.RoleIdManager, AuthSeedConstants.PermissionIdInvitationsView),
            RolePermission.Create(AuthSeedConstants.RoleIdManager, AuthSeedConstants.PermissionIdInvitationsAdd),
            RolePermission.Create(AuthSeedConstants.RoleIdManager, AuthSeedConstants.PermissionIdCardsView),
            RolePermission.Create(AuthSeedConstants.RoleIdManager, AuthSeedConstants.PermissionIdRewardsView),
            RolePermission.Create(AuthSeedConstants.RoleIdManager, AuthSeedConstants.PermissionIdScanStamping),
            RolePermission.Create(AuthSeedConstants.RoleIdManager, AuthSeedConstants.PermissionIdScanRedeem),
            RolePermission.Create(AuthSeedConstants.RoleIdManager, AuthSeedConstants.PermissionIdCardsAdd),

            // User (4)
            RolePermission.Create(AuthSeedConstants.RoleIdUser, AuthSeedConstants.PermissionIdTenantsView),
            RolePermission.Create(AuthSeedConstants.RoleIdUser, AuthSeedConstants.PermissionIdTenantsSetup),
            RolePermission.Create(AuthSeedConstants.RoleIdUser, AuthSeedConstants.PermissionIdCardsView),
            RolePermission.Create(AuthSeedConstants.RoleIdUser, AuthSeedConstants.PermissionIdRewardsView)
        ]);
    }
}
