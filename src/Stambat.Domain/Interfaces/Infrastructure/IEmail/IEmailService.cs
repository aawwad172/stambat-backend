using Stambat.Domain.ValueObjects;

namespace Stambat.Domain.Interfaces.Infrastructure.IEmail;

public interface IEmailService
{
    Task SendEmailAsync(Email email);
    Task SendVerificationEmailAsync(string to, string userName, string link);
    Task SendExistingUserAccessGrantAsync(
        string to,
        string inviteeName,
        string role,
        string tenantName,
        string dashboardLink);
    Task SendMerchantOnboardingInviteAsync(
        string to,
        string tenantName,
        string link,
        int expiresAfterDays,
        CancellationToken cancellationToken = default);
    Task SendTenantWelcomeEmailAsync(string to, string ownerName, string businessName, string dashboardLink);
}
