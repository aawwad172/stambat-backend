using FluentEmail.Core;

using Stamply.Domain.Interfaces.Infrastructure.IEmail;

namespace Stamply.Infrastructure.Email;

public class EmailService(IFluentEmail fluentEmail) : IEmailService
{
    private readonly IFluentEmail _fluentEmail = fluentEmail;
    public async Task SendEmailAsync(Domain.ValueObjects.Email email)
    {
        await _fluentEmail
                .To(email.To)
                .Subject(email.Subject)
                .Body(email.Body, isHtml: true)
                .SendAsync();
    }
}
