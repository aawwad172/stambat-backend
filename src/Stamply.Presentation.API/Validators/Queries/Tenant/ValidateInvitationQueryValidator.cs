using System;

using FluentValidation;

using Stamply.Application.CQRS.Queries.Tenant;

namespace Stamply.Presentation.API.Validators.Queries.Tenant;

public class ValidateInvitationQueryValidator : AbstractValidator<ValidateInvitationQuery>
{
    public ValidateInvitationQueryValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty()
            .NotNull();
    }
}
