using FluentValidation;

using Stambat.Application.CQRS.Commands.Tenants;

namespace Stambat.WebAPI.Validators.Commands.Tenants;

public class UpdateStaffRolesCommandValidator : AbstractValidator<UpdateStaffRolesCommand>
{
    public UpdateStaffRolesCommandValidator()
    {
        RuleFor(x => x.StaffId)
            .NotEmpty()
            .WithMessage("StaffId is required.");

        RuleFor(x => x.Roles)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Roles list must not be empty.")
            .Must(roles => roles.Any(r => r.IsSelected))
            .WithMessage("At least one role must be selected.");

        RuleForEach(x => x.Roles).ChildRules(role =>
            role.RuleFor(r => r.RoleId)
                .NotEmpty()
                .WithMessage("Each role must have a valid RoleId."));
    }
}
