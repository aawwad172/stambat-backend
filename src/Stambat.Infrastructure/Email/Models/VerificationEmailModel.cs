namespace Stambat.Infrastructure.Email.Models;

public class VerificationEmailModel
{
    public string UserName { get; set; } = string.Empty;
    public string VerificationLink { get; set; } = string.Empty;
}
