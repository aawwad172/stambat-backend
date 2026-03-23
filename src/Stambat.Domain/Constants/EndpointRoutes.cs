namespace Stambat.Domain.Constants;

public static class EndpointRoutes
{
    // Auth & Session
    public const string Login = "/login";
    public const string Logout = "/logout";
    public const string RefreshToken = "/refresh-token";

    // User / Profile
    public const string RegisterUser = "/register";
    public const string VerifyEmail = "/verify-email";
    public const string IsVerified = "/is-verified";

    // Invitations (Public/Token-based)
    public const string ValidateInvitation = "/validate";
    public const string AcceptInvitation = "/accept";

    // Tenant Operations (Merchant/Owner context)
    public const string SetupTenant = "/setup";
    public const string InviteStaff = "/staff/invitations"; // Plural & clear
    public const string GetAllTenantStaff = "/staff";

    // System Admin (Platform Owner only)
    public const string InviteTenant = "/tenants/onboarding";
}
