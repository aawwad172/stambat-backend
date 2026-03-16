using FluentValidation;

using Stamply.Application.CQRS.Queries.Invitations;

namespace Stamply.Presentation.API.Validators.Queries.Invitations;

public class ValidateInvitationQueryValidator : AbstractValidator<ValidateInvitationQuery>
{
    public ValidateInvitationQueryValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty()
            .WithMessage("Token is required");
    }
}
