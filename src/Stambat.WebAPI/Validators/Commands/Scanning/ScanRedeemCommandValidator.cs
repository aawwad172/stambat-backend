using FluentValidation;

using Stambat.Application.CQRS.Commands.Scanning;

namespace Stambat.WebAPI.Validators.Commands.Scanning;

public class ScanRedeemCommandValidator : AbstractValidator<ScanRedeemCommand>
{
    public ScanRedeemCommandValidator()
    {
        RuleFor(x => x.QrToken)
            .NotEmpty()
            .WithMessage("QR token is required");
    }
}
