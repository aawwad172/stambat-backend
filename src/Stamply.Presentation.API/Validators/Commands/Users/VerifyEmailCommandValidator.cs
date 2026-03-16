using FluentValidation;

using Stamply.Application.CQRS.Commands.Users;

namespace Stamply.Presentation.API.Validators.Commands.Users;

public class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
{
    public VerifyEmailCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User Id is required");

        RuleFor(x => x.Token)
            .NotEmpty()
            .WithMessage("Token is required");
    }
}
