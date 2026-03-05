using FluentValidation;

using Stamply.Application.CQRS.Commands.Tenant;

namespace Stamply.Presentation.API.Validators.Commands.Tenant;

public class SetupTenantCommandValidator : AbstractValidator<SetupTenantCommand>
{
    public SetupTenantCommandValidator()
    {
        throw new NotImplementedException();
    }
}
