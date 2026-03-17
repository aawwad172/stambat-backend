namespace Stambat.Infrastructure.Configurations.Seed;

public static class AuthSeedConstants
{
    // WARNING: These GUIDs are placeholder examples. 
    // They are unique, static, and fixed, which is what EF Core needs.

    // --- System & Admin User IDs ---
    // User ID: System/Automation User
    public static readonly Guid SystemUserId = new("A0000000-0000-7000-8000-000000000000");

    public static readonly string SystemSecurityStampGuid = "0199ecd3-b844-792f-8f83-431df66c629d";

    // --- Role IDs ---
    // Role ID: SuperAdmin
    public static readonly Guid RoleIdSuperAdmin = new("019cd46a-80a8-76a2-b7eb-20ca5903c25e");

    // Role ID: Admin
    public static readonly Guid RoleIdAdmin = new("019cd46a-80b3-7eb0-9861-254e15e297db");

    // Role ID: Merchant
    public static readonly Guid RoleIdMerchant = new("019cd46a-80b3-712e-8b82-edbae70f6a0d");

    // Role ID: Manager
    public static readonly Guid RoleIdManager = new("019cd46a-80b3-7a1a-a1b1-254e15e297db");

    // Role ID: Standard User
    public static readonly Guid RoleIdUser = new("019cd46a-80b3-7d68-8834-46e510948741");

    // --- Initial Permissions ---
    // Tenants
    public static readonly Guid PermissionIdTenantsView = new("019cd45e-7cf2-7f36-9595-2bd07dc9e25b");
    public static readonly Guid PermissionIdTenantsAdd = new("019cd45e-7cfc-7f11-9cdc-4581e5854eac");
    public static readonly Guid PermissionIdTenantsEdit = new("019cd45e-7cfc-7839-a733-b7b1c30138d4");
    public static readonly Guid PermissionIdTenantsDelete = new("019cd45e-7cfc-7807-8f5e-f4cce0b1ee3a");
    public static readonly Guid PermissionIdTenantsSetup = new("019cd47e-8346-7a3e-9c1c-f0d3a65d4091");

    // Users
    public static readonly Guid PermissionIdUsersView = new("019cd45e-7cfc-7947-88e3-7c06f899920f");
    public static readonly Guid PermissionIdUsersAdd = new("019cd45e-7cfc-78d6-8548-e20dbdadc04b");
    public static readonly Guid PermissionIdUsersEdit = new("019cd45e-7cfc-76d3-b56e-cc03d8854ff4");
    public static readonly Guid PermissionIdUsersDelete = new("019cd45e-7cfc-70e9-895b-4175ca7f5ee6");

    // Invitations
    public static readonly Guid PermissionIdInvitationsView = new("019cd45e-7cfc-717e-b9df-56b9565cc5e1");
    public static readonly Guid PermissionIdInvitationsAdd = new("019cd45e-7cfc-7ef1-9e1a-8ae8e3ff78f6");
    public static readonly Guid PermissionIdInvitationsEdit = new("019cd45e-7cfc-717f-853f-ed4d8c572e50");
    public static readonly Guid PermissionIdInvitationsDelete = new("019cd45e-7cfc-7156-83d3-81d9e8951b34");

    // Cards
    public static readonly Guid PermissionIdCardsView = new("019cd45e-7cfc-71fd-b726-7446578cf8b8");
    public static readonly Guid PermissionIdCardsAdd = new("019cd45e-7cfc-75ec-9acf-136b67adda34");
    public static readonly Guid PermissionIdCardsEdit = new("019cd45e-7cfc-7499-a257-849fd24c2871");
    public static readonly Guid PermissionIdCardsDelete = new("019cd45e-7cfc-79ad-8fd5-cf99cd27ad06");

    // Rewards
    public static readonly Guid PermissionIdRewardsView = new("019cd45e-7cfc-7144-85d7-a624389a5bda");
    public static readonly Guid PermissionIdRewardsAdd = new("019cd45e-7cfc-7af3-8e82-501087bdc3fd");
    public static readonly Guid PermissionIdRewardsEdit = new("019cd45e-7cfc-739a-9ceb-77a6fae361a7");
    public static readonly Guid PermissionIdRewardsDelete = new("019cd45e-7cfc-72ca-892a-4c5484b063af");

    // Scan
    public static readonly Guid PermissionIdScanStamping = new("019cd45e-7cfc-78ba-b9fc-d69399cdc202");
    public static readonly Guid PermissionIdScanRedeem = new("019cd45e-7cfc-735f-ac03-e6b5ebe67f6e");

    // Super Admin
    public static readonly Guid PermissionIdSystemManage = new("019cd465-18f5-7e38-951f-99444e306980");
    public static readonly Guid PermissionIdSystemLogsView = new("019cd465-18ff-7956-a606-0c8f350e2330");
    public static readonly Guid PermissionIdSystemAuditView = new("019cd465-18ff-7481-be6f-34e04e75f169");
    public static readonly Guid PermissionIdSystemSettingsEdit = new("019cd465-18ff-77ba-b57f-2026a084e84a");
    public static readonly Guid PermissionIdTenantsManage = new("019cd465-18ff-733b-9eed-eaadb6650b4c");

    public static readonly DateTime SeedDateUtc = new(2025, 10, 15, 0, 0, 0, DateTimeKind.Utc);

}
