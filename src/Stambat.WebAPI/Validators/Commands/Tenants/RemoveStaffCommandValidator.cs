using FluentValidation;

using Stambat.Application.CQRS.Commands.Tenants;

namespace Stambat.WebAPI.Validators.Commands.Tenants;

public class RemoveStaffCommandValidator : AbstractValidator<RemoveStaffCommand>
{
    public RemoveStaffCommandValidator()
    {
        RuleFor(x => x.StaffId)
            .NotEmpty()
            .WithMessage("StaffId is required.");
    }
}
