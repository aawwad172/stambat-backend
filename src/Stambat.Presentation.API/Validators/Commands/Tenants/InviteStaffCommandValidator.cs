using FluentValidation;

using Stambat.Application.CQRS.Commands.Tenants;
using Stambat.Domain.Enums;

namespace Stambat.WebAPI.Validators.Commands.Tenants;

public class InviteStaffCommandValidator : AbstractValidator<InviteStaffCommand>
{
    public InviteStaffCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("A valid email address is required");

        RuleFor(x => x.Role)
            .NotEmpty()
            .WithMessage("Role is required")
            .Must(role => role is RolesEnum.Merchant or RolesEnum.Manager)
            .WithMessage("Only Merchant or Manager roles can be invited through this endpoint");
    }
}
