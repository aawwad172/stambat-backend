using FluentValidation;

using Stamply.Application.CQRS.Commands.Authentication;

namespace Stamply.Presentation.API.Validators.Commands.Authentication;

public class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
{
    public VerifyEmailCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Token)
            .NotNull()
            .NotEmpty();
    }
}
