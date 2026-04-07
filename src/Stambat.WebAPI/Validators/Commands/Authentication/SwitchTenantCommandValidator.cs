using FluentValidation;

using Stambat.Application.CQRS.Commands.Authentication;

namespace Stambat.WebAPI.Validators.Commands.Authentication;

public class SwitchTenantCommandValidator : AbstractValidator<SwitchTenantCommand>
{
    public SwitchTenantCommandValidator()
    {
        RuleFor(x => x.TenantId)
            .NotEmpty()
            .WithMessage("Tenant ID is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("Tenant ID must be a valid GUID.");
    }
}
