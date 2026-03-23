using FluentEmail.Core;

using Stambat.Domain.Interfaces.Infrastructure.IEmail;
using Stambat.Domain.ValueObjects;
using Stambat.Infrastructure.Email.Models;

namespace Stambat.Infrastructure.Email;

public class EmailService(IFluentEmail fluentEmail) : IEmailService
{
    private readonly IFluentEmail _fluentEmail = fluentEmail;
    // Helper to resolve paths correctly across different environments (Mac/Linux)
    private string GetTemplatePath(string templateName)
        => Path.Combine("../Stambat.Infrastructure/", "Email", "Templates", templateName);
    public async Task SendEmailAsync(EmailMessage email)
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
            .Subject("Verify your email - Stambat")
            .UsingTemplateFromFile(path, model)
            .SendAsync();
    }

    // Method for inviting team members (Administrator role)
    public async Task SendExistingUserAccessGrantAsync(string to, string inviteeName, string role, string tenantName, string dashboardLink)
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
            .Subject($"Access Granted: You are now a {role} for {tenantName}")
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
            .Subject($"Welcome to Stambat, {businessName}!")
            .UsingTemplateFromFile(path, model)
            .SendAsync();
    }

    public async Task SendMerchantOnboardingInviteAsync(
        string to,
        string tenantName,
        string link,
        int expiresAfterDays,
        CancellationToken cancellationToken = default)
    {
        var model = new MerchantInvitationEmailModel
        {
            Email = to,
            TenantName = tenantName,
            InvitationLink = link,
            ExpiresInDays = expiresAfterDays
        };

        string path = GetTemplatePath("MerchantInvitation.cshtml");

        if (!File.Exists(path))
            throw new FileNotFoundException("Onboarding invitation template not found", path);

        await _fluentEmail
            .To(to)
            .Subject("Action Required: Join your team on Stambat")
            .UsingTemplateFromFile(path, model)
            .SendAsync(cancellationToken);
    }
}
