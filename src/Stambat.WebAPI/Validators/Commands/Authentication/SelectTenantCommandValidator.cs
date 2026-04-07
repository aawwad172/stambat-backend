using FluentValidation;

using Stambat.Application.CQRS.Commands.Authentication;

namespace Stambat.WebAPI.Validators.Commands.Authentication;

public class SelectTenantCommandValidator : AbstractValidator<SelectTenantCommand>
{
    public SelectTenantCommandValidator()
    {
        RuleFor(x => x.IdentityToken)
            .NotEmpty()
            .WithMessage("Identity token is required.");

        RuleFor(x => x.TenantId)
            .NotEmpty()
            .WithMessage("Tenant ID is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("Tenant ID must be a valid GUID.");
    }
}
