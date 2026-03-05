using Stamply.Domain.ValueObjects;

namespace Stamply.Domain.Interfaces.Infrastructure.IEmail;

public interface IEmailService
{
    Task SendEmailAsync(Email email);
}
