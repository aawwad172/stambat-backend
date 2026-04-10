using FluentValidation;

using Stambat.Application.CQRS.Commands.Scanning;

namespace Stambat.WebAPI.Validators.Commands.Scanning;

public class ScanStampCommandValidator : AbstractValidator<ScanStampCommand>
{
    public ScanStampCommandValidator()
    {
        RuleFor(x => x.QrToken)
            .NotEmpty()
            .WithMessage("QR token is required");

        RuleFor(x => x.StampsToAdd)
            .GreaterThan(0)
            .WithMessage("Stamps to add must be greater than 0")
            .LessThanOrEqualTo(5)
            .WithMessage("Cannot add more than 5 stamps at once");

        RuleFor(x => x.Note)
            .MaximumLength(250)
            .When(x => x.Note is not null)
            .WithMessage("Note must not exceed 250 characters");
    }
}
