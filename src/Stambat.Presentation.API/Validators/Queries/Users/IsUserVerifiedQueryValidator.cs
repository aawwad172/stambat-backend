using FluentValidation;

using Stambat.Application.CQRS.Queries.Users;

namespace Stambat.WebAPI.Validators.Queries.Users;

public class IsUserVerifiedQueryValidator : AbstractValidator<IsUserVerifiedQuery>
{
    public IsUserVerifiedQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User Id is required");
    }
}
