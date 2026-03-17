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
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdTenantsView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdTenantsAdd },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdTenantsEdit },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdTenantsDelete },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdTenantsSetup },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdUsersView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdUsersAdd },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdUsersEdit },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdUsersDelete },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdInvitationsView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdInvitationsAdd },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdInvitationsEdit },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdInvitationsDelete },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdCardsView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdCardsAdd },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdCardsEdit },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdCardsDelete },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdRewardsView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdRewardsAdd },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdRewardsEdit },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdRewardsDelete },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdScanStamping },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdScanRedeem },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdSystemManage },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdSystemLogsView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdSystemAuditView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdSystemSettingsEdit },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdSuperAdmin, PermissionId = AuthSeedConstants.PermissionIdTenantsManage },

            // TenantAdmin (20)
            new RolePermission { RoleId = AuthSeedConstants.RoleIdAdmin, PermissionId = AuthSeedConstants.PermissionIdTenantsView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdAdmin, PermissionId = AuthSeedConstants.PermissionIdTenantsEdit },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdAdmin, PermissionId = AuthSeedConstants.PermissionIdUsersView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdAdmin, PermissionId = AuthSeedConstants.PermissionIdUsersAdd },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdAdmin, PermissionId = AuthSeedConstants.PermissionIdUsersEdit },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdAdmin, PermissionId = AuthSeedConstants.PermissionIdUsersDelete },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdAdmin, PermissionId = AuthSeedConstants.PermissionIdInvitationsView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdAdmin, PermissionId = AuthSeedConstants.PermissionIdInvitationsAdd },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdAdmin, PermissionId = AuthSeedConstants.PermissionIdInvitationsEdit },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdAdmin, PermissionId = AuthSeedConstants.PermissionIdInvitationsDelete },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdAdmin, PermissionId = AuthSeedConstants.PermissionIdCardsView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdAdmin, PermissionId = AuthSeedConstants.PermissionIdCardsAdd },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdAdmin, PermissionId = AuthSeedConstants.PermissionIdCardsEdit },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdAdmin, PermissionId = AuthSeedConstants.PermissionIdCardsDelete },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdAdmin, PermissionId = AuthSeedConstants.PermissionIdRewardsView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdAdmin, PermissionId = AuthSeedConstants.PermissionIdRewardsAdd },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdAdmin, PermissionId = AuthSeedConstants.PermissionIdRewardsEdit },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdAdmin, PermissionId = AuthSeedConstants.PermissionIdRewardsDelete },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdAdmin, PermissionId = AuthSeedConstants.PermissionIdScanStamping },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdAdmin, PermissionId = AuthSeedConstants.PermissionIdScanRedeem },

            // Merchant (6)
            new RolePermission { RoleId = AuthSeedConstants.RoleIdMerchant, PermissionId = AuthSeedConstants.PermissionIdTenantsView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdMerchant, PermissionId = AuthSeedConstants.PermissionIdUsersView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdMerchant, PermissionId = AuthSeedConstants.PermissionIdCardsView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdMerchant, PermissionId = AuthSeedConstants.PermissionIdRewardsView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdMerchant, PermissionId = AuthSeedConstants.PermissionIdScanStamping },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdMerchant, PermissionId = AuthSeedConstants.PermissionIdScanRedeem },

            // Manager (10)
            new RolePermission { RoleId = AuthSeedConstants.RoleIdManager, PermissionId = AuthSeedConstants.PermissionIdTenantsView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdManager, PermissionId = AuthSeedConstants.PermissionIdUsersView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdManager, PermissionId = AuthSeedConstants.PermissionIdUsersAdd },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdManager, PermissionId = AuthSeedConstants.PermissionIdInvitationsView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdManager, PermissionId = AuthSeedConstants.PermissionIdInvitationsAdd },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdManager, PermissionId = AuthSeedConstants.PermissionIdCardsView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdManager, PermissionId = AuthSeedConstants.PermissionIdRewardsView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdManager, PermissionId = AuthSeedConstants.PermissionIdScanStamping },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdManager, PermissionId = AuthSeedConstants.PermissionIdScanRedeem },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdManager, PermissionId = AuthSeedConstants.PermissionIdCardsAdd },

            // User (4)
            new RolePermission { RoleId = AuthSeedConstants.RoleIdUser, PermissionId = AuthSeedConstants.PermissionIdTenantsView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdUser, PermissionId = AuthSeedConstants.PermissionIdTenantsSetup },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdUser, PermissionId = AuthSeedConstants.PermissionIdCardsView },
            new RolePermission { RoleId = AuthSeedConstants.RoleIdUser, PermissionId = AuthSeedConstants.PermissionIdRewardsView }
        ]);
    }
}
