using System.Data;

using FluentValidation;

using Stamply.Application.CQRS.Commands.Tenant;

namespace Stamply.Presentation.API.Validators.Commands.Tenant;

public class SetupTenantCommandValidator : AbstractValidator<SetupTenantCommand>
{
    public SetupTenantCommandValidator()
    {
        RuleFor(x => x.BusinessEmail)
            .EmailAddress()
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .NotNull();
    }
}
