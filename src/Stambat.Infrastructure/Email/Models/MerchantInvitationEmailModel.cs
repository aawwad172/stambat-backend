namespace Stambat.Infrastructure.Email.Models;

public class MerchantInvitationEmailModel
{
    public required string TenantName { get; set; }
    public required string InvitationLink { get; set; }
    public required string Email { get; set; }
    public int ExpiresInDays { get; set; }
}
