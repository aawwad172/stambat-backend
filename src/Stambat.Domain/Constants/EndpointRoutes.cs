namespace Stambat.Domain.Constants;

public static class EndpointRoutes
{
    // Auth & Session
    public const string Login = "/login";
    public const string Logout = "/logout";
    public const string RefreshToken = "/refresh-token";
    public const string SelectTenant = "/select-tenant";
    public const string SwitchTenant = "/switch-tenant";

    // User / Profile
    public const string RegisterUser = "/register";
    public const string VerifyEmail = "/verify-email";
    public const string IsVerified = "/is-verified";

    // Invitations (Public/Token-based)
    public const string ValidateInvitation = "/validate";
    public const string AcceptInvitation = "/accept";
    public const string JoinTenant = "/join";

    // Tenant Operations (Merchant/Owner context)
    public const string SetupTenant = "/setup";
    public const string StaffInvitations = "/staff/invitations"; // GET = list, POST = invite, DELETE = cancel
    public const string InviteStaff = StaffInvitations;
    public const string GetAllTenantStaff = "/staff";
    public const string GetStaffMember = "/staff/details";
    public const string RemoveStaff = "/staff";
    public const string DeactivateStaff = "/staff/deactivate";
    public const string StaffRoles = "/staff/roles"; // GET = catalog, PATCH = update assignments

    // Cards (under /cards group)
    public const string GetAllCards = "/list";
    public const string CreateCard = "/create";
    public const string GetCardById = "/details";
    public const string UpdateCard = "/update";
    public const string DeleteCard = "/delete";

    // System Admin (Platform Owner only)
    public const string InviteTenant = "/tenants/onboarding";

    // Customer Onboarding (Public)
    public const string CustomerOnboard = "/join";

    // Scanning (Authenticated Staff)
    public const string ScanStamp = "/stamp";
    public const string ScanRedeem = "/redeem";

    // Wallet Pass Management
    public const string GetWalletPassDetails = "/pass/details";
    public const string GetGoogleWalletUrl = "/google/{walletPassId}";
}
