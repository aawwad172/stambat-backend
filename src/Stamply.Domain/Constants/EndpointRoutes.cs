namespace Stamply.Domain.Constants;

public static class EndpointRoutes
{
    // Auth & Session
    public const string Login = "/auth/login";
    public const string Logout = "/auth/logout";
    public const string RefreshToken = "/auth/refresh-token";

    // User / Profile
    public const string RegisterUser = "/users/register";
    public const string VerifyEmail = "/users/verify-email";
    public const string IsVerified = "/users/is-verified";

    // Invitations (Public/Token-based)
    public const string ValidateInvitation = "/invitations/validate";
    public const string AcceptInvitation = "/invitations/accept";

    // Tenant Operations (Merchant/Owner context)
    public const string SetupTenant = "/tenants/setup";
    public const string InviteMerchant = "/tenants/invitations"; // Plural & clear

    // System Admin (Platform Owner only)
    public const string InviteTenant = "/admin/tenants/onboarding";
}
