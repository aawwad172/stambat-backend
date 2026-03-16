using FluentValidation;

using Stamply.Application.CQRS.Commands.Tenant;
using Stamply.Domain.Enums;

namespace Stamply.Presentation.API.Validators.Commands.Tenant;

public class InviteMerchantCommandValidator : AbstractValidator<InviteMerchantCommand>
{
    public InviteMerchantCommandValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Role)
            .NotNull()
            .NotEmpty()
            .IsInEnum();
    }
}
