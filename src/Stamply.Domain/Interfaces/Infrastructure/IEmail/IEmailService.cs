using Stamply.Domain.ValueObjects;

namespace Stamply.Domain.Interfaces.Infrastructure.IEmail;

public interface IEmailService
{
    Task SendEmailAsync(Email email);
    Task SendVerificationEmailAsync(string to, string userName, string link);
    Task SendTeamInvitationEmailAsync(string to, string inviteeName, string role, string tenantName, string dashboardLink);
    Task SendTenantWelcomeEmailAsync(string to, string ownerName, string businessName, string dashboardLink);
}
