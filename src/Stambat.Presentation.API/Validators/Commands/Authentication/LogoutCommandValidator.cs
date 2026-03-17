using Stambat.Application.CQRS.Commands.Authentication;

using FluentValidation;

namespace Stambat.WebAPI.Validators.Commands.Authentication;

public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage("Refresh token is required.");
    }
}
