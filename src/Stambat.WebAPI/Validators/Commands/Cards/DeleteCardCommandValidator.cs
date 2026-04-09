using FluentValidation;

using Stambat.Application.CQRS.Commands.Cards;

namespace Stambat.WebAPI.Validators.Commands.Cards;

public class DeleteCardCommandValidator : AbstractValidator<DeleteCardCommand>
{
    public DeleteCardCommandValidator()
    {
        RuleFor(x => x.CardTemplateId)
            .NotEmpty()
            .WithMessage("Card template ID is required");
    }
}
