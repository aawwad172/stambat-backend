using FluentValidation;

using Stambat.Application.CQRS.Queries.Cards;

namespace Stambat.WebAPI.Validators.Queries.Cards;

public class GetAllCardsQueryValidator : AbstractValidator<GetAllCardsQuery>
{
    private static readonly string[] AllowedSortFields = ["CreatedAt", "Title"];

    public GetAllCardsQueryValidator()
    {
        RuleFor(q => q.Page).GreaterThan(0);
        RuleFor(q => q.Size).InclusiveBetween(1, 50);

        RuleFor(q => q.SortBy)
            .Must(s => AllowedSortFields.Contains(s, StringComparer.OrdinalIgnoreCase))
            .WithMessage("SortBy must be 'CreatedAt' or 'Title'");
    }
}
