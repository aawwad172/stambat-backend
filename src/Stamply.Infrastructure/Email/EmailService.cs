using FluentEmail.Core;

using Stamply.Domain.Interfaces.Infrastructure.IEmail;
using Stamply.Infrastructure.Email.Models;

namespace Stamply.Infrastructure.Email;

public class EmailService(IFluentEmail fluentEmail) : IEmailService
{
    private readonly IFluentEmail _fluentEmail = fluentEmail;
    // Helper to resolve paths correctly across different environments (Mac/Linux)
    private string GetTemplatePath(string templateName)
        => Path.Combine("../Stamply.Infrastructure/", "Email", "Templates", templateName);
    public async Task SendEmailAsync(Domain.ValueObjects.Email email)
    {
        await _fluentEmail
                .To(email.To)
                .Subject(email.Subject)
                .Body(email.Body, isHtml: true)
                .SendAsync();
    }

    public async Task SendVerificationEmailAsync(string to, string userName, string link)
    {
        // Map raw strings to the internal model
        VerificationEmailModel model = new()
        {
            UserName = userName,
            VerificationLink = link
        };

        // TODO: We need to find a better way to handle the path of the template
        string path = GetTemplatePath("VerificationEmail.cshtml");
        if (!File.Exists(path)) throw new FileNotFoundException("Template not found", path);

        await _fluentEmail
            .To(to)
            .Subject("Verify your email - Stamply")
            .UsingTemplateFromFile(path, model)
            .SendAsync();
    }

    // Method for inviting team members (Administrator role)
    public async Task SendTeamInvitationEmailAsync(string to, string inviteeName, string role, string tenantName, string dashboardLink)
    {
        TeamInvitationEmailModel model = new()
        {
            InviteeName = inviteeName,
            Role = role,
            TenantName = tenantName,
            DashboardLink = dashboardLink
        };

        string path = GetTemplatePath("TeamInvitation.cshtml");
        if (!File.Exists(path)) throw new FileNotFoundException("Template not found", path);

        await _fluentEmail
            .To(to)
            .Subject($"You've been invited to join {tenantName} on Stamply")
            .UsingTemplateFromFile(path, model)
            .SendAsync();
    }

    // Method for welcoming the Tenant/Business owner after Step 5
    public async Task SendTenantWelcomeEmailAsync(string to, string ownerName, string businessName, string dashboardLink)
    {
        TenantWelcomeEmailModel model = new()
        {
            OwnerName = ownerName,
            BusinessName = businessName,
            DashboardLink = dashboardLink
        };

        string path = GetTemplatePath("TenantWelcome.cshtml");
        if (!File.Exists(path)) throw new FileNotFoundException("Template not found", path);

        await _fluentEmail
            .To(to)
            .Subject($"Welcome to Stamply, {businessName}!")
            .UsingTemplateFromFile(path, model)
            .SendAsync();
    }
}
