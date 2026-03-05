using System;

namespace Stamply.Domain.Constants;

public static class EndpointRoutes
{
    public const string RegisterUser = "/users/register";
    public const string Login = "/users/login";
    public const string RefreshToken = "/users/refresh-token";
    public const string Logout = "/users/logout";
    public const string InviteTenant = "admin/tenants/invitations";
    public const string InviteMerchant = "tenants/{tenantId}/users/invitations";
    public const string SetupTenant = "tenants/setup";
}
