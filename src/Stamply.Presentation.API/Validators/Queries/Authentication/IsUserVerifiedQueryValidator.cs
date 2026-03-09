using FluentValidation;

using Stamply.Application.CQRS.Queries.Authentication;

namespace Stamply.Presentation.API.Validators.Queries.Authentication;

public class IsUserVerifiedQueryValidator : AbstractValidator<IsUserVerifiedQuery>
{
    public IsUserVerifiedQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}
