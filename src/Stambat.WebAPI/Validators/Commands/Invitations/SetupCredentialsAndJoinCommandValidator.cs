using FluentValidation;

using Stambat.Application.CQRS.Commands.Invitations;

namespace Stambat.WebAPI.Validators.Commands.Invitations;

public class SetupCredentialsAndJoinCommandValidator : AbstractValidator<SetupCredentialsAndJoinCommand>
{
    public SetupCredentialsAndJoinCommandValidator()
    {
        RuleFor(x => x.Username)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Username is required.")
            .Must(username => username == username.ToLowerInvariant())
            .WithMessage("Username must be in lowercase.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long.")
            .Matches(@"^(?=.*[a-z])")
            .WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"^(?=.*[A-Z])")
            .WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"^(?=.*\d)")
            .WithMessage("Password must contain at least one number.")
            .Matches(@"^(?=.*[^\da-zA-Z])")
            .WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.Token)
            .NotEmpty()
            .WithMessage("Invitation token is required.");
    }
}
