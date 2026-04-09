using FluentValidation;

using Stambat.Application.CQRS.Commands.Tenants;

namespace Stambat.WebAPI.Validators.Commands.Tenants;

public class DeactivateStaffCommandValidator : AbstractValidator<DeactivateStaffCommand>
{
    public DeactivateStaffCommandValidator()
    {
        RuleFor(x => x.StaffId)
            .NotEmpty()
            .WithMessage("StaffId is required.");
    }
}
