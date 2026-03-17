using Stambat.Application.CQRS.Commands.Authentication;

using FluentValidation;

namespace Stambat.WebAPI.Validators.Commands.Authentication;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage("Refresh token is required.");
    }
}
