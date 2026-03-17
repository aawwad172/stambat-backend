namespace Stambat.Infrastructure.Email.Models;

public class TeamInvitationEmailModel
{
    public string InviteeName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string TenantName { get; set; } = string.Empty;
    public string DashboardLink { get; set; } = string.Empty;
}
