using FluentValidation;

using Stamply.Application.CQRS.Queries.Users;

namespace Stamply.Presentation.API.Validators.Queries.Users;

public class IsUserVerifiedQueryValidator : AbstractValidator<IsUserVerifiedQuery>
{
    public IsUserVerifiedQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User Id is required");
    }
}
