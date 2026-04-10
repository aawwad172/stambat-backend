using FluentValidation;

using Stambat.Application.CQRS.Commands.Wallet;

namespace Stambat.WebAPI.Validators.Commands.Wallet;

public class CustomerOnboardCommandValidator : AbstractValidator<CustomerOnboardCommand>
{
    public CustomerOnboardCommandValidator()
    {
        RuleFor(x => x.CardTemplateId)
            .NotEmpty()
            .WithMessage("Card template ID is required");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required")
            .MaximumLength(50)
            .WithMessage("First name must not exceed 50 characters");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required")
            .MaximumLength(50)
            .WithMessage("Last name must not exceed 50 characters");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("A valid email address is required");

        RuleFor(x => x.Phone)
            .MaximumLength(20)
            .When(x => x.Phone is not null)
            .WithMessage("Phone number must not exceed 20 characters");

        RuleFor(x => x.WalletProvider)
            .IsInEnum()
            .WithMessage("A valid wallet provider type is required");
    }
}
