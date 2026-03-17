namespace Stambat.Infrastructure.Email.Models;

public class TenantWelcomeEmailModel
{
    public string OwnerName { get; set; } = string.Empty;
    public string BusinessName { get; set; } = string.Empty;
    public string DashboardLink { get; set; } = string.Empty;
}
