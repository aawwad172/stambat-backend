using FluentValidation;

using Stambat.Application.CQRS.Commands.Invitations;

namespace Stambat.WebAPI.Validators.Commands.Invitations;

public class JoinTenantCommandValidator : AbstractValidator<JoinTenantCommand>
{
    public JoinTenantCommandValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required to verify your identity.");

        RuleFor(x => x.Token)
            .NotEmpty()
            .WithMessage("Invitation token is required.");
    }
}
