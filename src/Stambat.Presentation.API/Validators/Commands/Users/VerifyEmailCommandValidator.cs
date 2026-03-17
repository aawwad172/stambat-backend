using FluentValidation;

using Stambat.Application.CQRS.Commands.Users;

namespace Stambat.WebAPI.Validators.Commands.Users;

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
