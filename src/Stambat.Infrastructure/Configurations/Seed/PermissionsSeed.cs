using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Enums;

namespace Stambat.Infrastructure.Configurations.Seed;

public class PermissionsSeed : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        // TODO: Add the desired Permissions.
        builder.HasData([
            // Tenants
            new Permission { Id = AuthSeedConstants.PermissionIdTenantsView, Name = PermissionConstants.TenantsView, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },
            new Permission { Id = AuthSeedConstants.PermissionIdTenantsAdd, Name = PermissionConstants.TenantsAdd, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },
            new Permission { Id = AuthSeedConstants.PermissionIdTenantsEdit, Name = PermissionConstants.TenantsEdit, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },
            new Permission { Id = AuthSeedConstants.PermissionIdTenantsDelete, Name = PermissionConstants.TenantsDelete, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },
            new Permission { Id = AuthSeedConstants.PermissionIdTenantsSetup, Name = PermissionConstants.TenantsSetup, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },

            // Users
            new Permission { Id = AuthSeedConstants.PermissionIdUsersView, Name = PermissionConstants.UsersView, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },
            new Permission { Id = AuthSeedConstants.PermissionIdUsersAdd, Name = PermissionConstants.UsersAdd, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },
            new Permission { Id = AuthSeedConstants.PermissionIdUsersEdit, Name = PermissionConstants.UsersEdit, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },
            new Permission { Id = AuthSeedConstants.PermissionIdUsersDelete, Name = PermissionConstants.UsersDelete, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },

            // Invitations
            new Permission { Id = AuthSeedConstants.PermissionIdInvitationsView, Name = PermissionConstants.InvitationsView, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },
            new Permission { Id = AuthSeedConstants.PermissionIdInvitationsAdd, Name = PermissionConstants.InvitationsAdd, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },
            new Permission { Id = AuthSeedConstants.PermissionIdInvitationsEdit, Name = PermissionConstants.InvitationsEdit, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },
            new Permission { Id = AuthSeedConstants.PermissionIdInvitationsDelete, Name = PermissionConstants.InvitationsDelete, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },

            // Cards
            new Permission { Id = AuthSeedConstants.PermissionIdCardsView, Name = PermissionConstants.CardsView, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },
            new Permission { Id = AuthSeedConstants.PermissionIdCardsAdd, Name = PermissionConstants.CardsAdd, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },
            new Permission { Id = AuthSeedConstants.PermissionIdCardsEdit, Name = PermissionConstants.CardsEdit, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },
            new Permission { Id = AuthSeedConstants.PermissionIdCardsDelete, Name = PermissionConstants.CardsDelete, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },

            // Rewards
            new Permission { Id = AuthSeedConstants.PermissionIdRewardsView, Name = PermissionConstants.RewardsView, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },
            new Permission { Id = AuthSeedConstants.PermissionIdRewardsAdd, Name = PermissionConstants.RewardsAdd, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },
            new Permission { Id = AuthSeedConstants.PermissionIdRewardsEdit, Name = PermissionConstants.RewardsEdit, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },
            new Permission { Id = AuthSeedConstants.PermissionIdRewardsDelete, Name = PermissionConstants.RewardsDelete, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },

            // Scan
            new Permission { Id = AuthSeedConstants.PermissionIdScanStamping, Name = PermissionConstants.ScanStamping, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },
            new Permission { Id = AuthSeedConstants.PermissionIdScanRedeem, Name = PermissionConstants.ScanRedeem, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },

            // Super Admin
            new Permission { Id = AuthSeedConstants.PermissionIdSystemManage, Name = PermissionConstants.SystemManage, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },
            new Permission { Id = AuthSeedConstants.PermissionIdSystemLogsView, Name = PermissionConstants.SystemLogsView, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },
            new Permission { Id = AuthSeedConstants.PermissionIdSystemAuditView, Name = PermissionConstants.SystemAuditView, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },
            new Permission { Id = AuthSeedConstants.PermissionIdSystemSettingsEdit, Name = PermissionConstants.SystemSettingsEdit, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId },
            new Permission { Id = AuthSeedConstants.PermissionIdTenantsManage, Name = PermissionConstants.TenantsManage, CreatedAt = AuthSeedConstants.SeedDateUtc, CreatedBy = AuthSeedConstants.SystemUserId }
        ]);
    }
}
