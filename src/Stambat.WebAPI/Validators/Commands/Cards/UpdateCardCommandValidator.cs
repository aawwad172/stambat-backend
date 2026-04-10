using System.Text.RegularExpressions;

using FluentValidation;

using Stambat.Application.CQRS.Commands.Cards;

namespace Stambat.WebAPI.Validators.Commands.Cards;

public partial class UpdateCardCommandValidator : AbstractValidator<UpdateCardCommand>
{
    [GeneratedRegex(@"^#([0-9A-Fa-f]{6}|[0-9A-Fa-f]{3})$")]
    private static partial Regex HexColorRegex();

    public UpdateCardCommandValidator()
    {
        RuleFor(x => x.CardTemplateId)
            .NotEmpty()
            .WithMessage("Card template ID is required");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Card title is required")
            .MaximumLength(100)
            .WithMessage("Card title must not exceed 100 characters");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Description must not exceed 500 characters");

        RuleFor(x => x.StampsRequired)
            .GreaterThan(0)
            .WithMessage("Stamps required must be greater than 0");

        RuleFor(x => x.RewardDescription)
            .MaximumLength(200)
            .WithMessage("Reward description must not exceed 200 characters");

        RuleFor(x => x.PrimaryColorOverride)
            .Matches(HexColorRegex())
            .When(x => x.PrimaryColorOverride is not null)
            .WithMessage("Primary color must be a valid hex color (e.g. #FF5733 or #F00)");

        RuleFor(x => x.SecondaryColorOverride)
            .Matches(HexColorRegex())
            .When(x => x.SecondaryColorOverride is not null)
            .WithMessage("Secondary color must be a valid hex color (e.g. #FF5733 or #F00)");

        RuleFor(x => x.LogoUrlOverride)
            .MaximumLength(500)
            .WithMessage("Logo URL must not exceed 500 characters");

        RuleFor(x => x.EmptyStampUrl)
            .MaximumLength(500)
            .WithMessage("Empty stamp URL must not exceed 500 characters");

        RuleFor(x => x.EarnedStampUrl)
            .MaximumLength(500)
            .WithMessage("Earned stamp URL must not exceed 500 characters");

        RuleFor(x => x.TermsAndConditions)
            .MaximumLength(2000)
            .WithMessage("Terms and conditions must not exceed 2000 characters");
    }
}
