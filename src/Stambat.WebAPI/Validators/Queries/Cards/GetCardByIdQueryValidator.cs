using FluentValidation;

using Stambat.Application.CQRS.Queries.Cards;

namespace Stambat.WebAPI.Validators.Queries.Cards;

public class GetCardByIdQueryValidator : AbstractValidator<GetCardByIdQuery>
{
    public GetCardByIdQueryValidator()
    {
        RuleFor(q => q.CardTemplateId)
            .NotEmpty()
            .WithMessage("Card template ID is required");
    }
}
