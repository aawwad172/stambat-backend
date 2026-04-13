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

        RuleFor(x => x.AmountToAdd)
            .GreaterThan(0)
            .WithMessage("Amount to add must be greater than 0")
            .LessThanOrEqualTo(100_000)
            .WithMessage("Amount to add must not exceed 100,000");

        RuleFor(x => x.Note)
            .MaximumLength(250)
            .When(x => x.Note is not null)
            .WithMessage("Note must not exceed 250 characters");
    }
}
