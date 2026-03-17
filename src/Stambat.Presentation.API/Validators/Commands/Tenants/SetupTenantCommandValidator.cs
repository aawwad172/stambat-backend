using FluentValidation;

using Stambat.Application.CQRS.Commands.Tenants;

namespace Stambat.WebAPI.Validators.Commands.Tenants;

public class SetupTenantCommandValidator : AbstractValidator<SetupTenantCommand>
{
    public SetupTenantCommandValidator()
    {
        RuleFor(x => x.BusinessEmail)
            .NotEmpty()
            .WithMessage("Business Email is required")
            .EmailAddress()
            .WithMessage("A valid Business Email is required");

        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .WithMessage("Company Name is required");
    }
}
