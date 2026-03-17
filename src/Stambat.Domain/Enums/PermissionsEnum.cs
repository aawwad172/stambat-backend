namespace Stambat.Domain.Enums;

public static class PermissionConstants
{
    // Tenants
    public const string TenantsView = "Tenants.View";
    public const string TenantsAdd = "Tenants.Add";
    public const string TenantsEdit = "Tenants.Edit";
    public const string TenantsDelete = "Tenants.Delete";
    public const string TenantsSetup = "Tenants.Setup";

    // Users
    public const string UsersView = "Users.View";
    public const string UsersAdd = "Users.Add";
    public const string UsersEdit = "Users.Edit";
    public const string UsersDelete = "Users.Delete";

    // Invitations
    public const string InvitationsView = "Invitations.View";
    public const string InvitationsAdd = "Invitations.Add";
    public const string InvitationsEdit = "Invitations.Edit";
    public const string InvitationsDelete = "Invitations.Delete";

    // Cards
    public const string CardsView = "Cards.View";
    public const string CardsAdd = "Cards.Add";
    public const string CardsEdit = "Cards.Edit";
    public const string CardsDelete = "Cards.Delete";

    // Rewards
    public const string RewardsView = "Rewards.View";
    public const string RewardsAdd = "Rewards.Add";
    public const string RewardsEdit = "Rewards.Edit";
    public const string RewardsDelete = "Rewards.Delete";

    // Scan
    public const string ScanStamping = "Scan.Stamping";
    public const string ScanRedeem = "Scan.Redeem";

    // System (Super Admin)
    public const string SystemManage = "System.Manage";
    public const string SystemLogsView = "System.Logs.View";
    public const string SystemAuditView = "System.Audit.View";
    public const string SystemSettingsEdit = "System.Settings.Edit";
    public const string TenantsManage = "Tenants.Manage";
}
