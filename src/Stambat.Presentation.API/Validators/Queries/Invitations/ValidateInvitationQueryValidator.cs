using FluentValidation;

using Stambat.Application.CQRS.Queries.Invitations;

namespace Stambat.WebAPI.Validators.Queries.Invitations;

public class ValidateInvitationQueryValidator : AbstractValidator<ValidateInvitationQuery>
{
    public ValidateInvitationQueryValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty()
            .WithMessage("Token is required");
    }
}
