using FluentValidation;

using Stambat.Application.CQRS.Commands.Tenants;

namespace Stambat.WebAPI.Validators;

public class CancelInvitationCommandValidator : AbstractValidator<CancelInvitationCommand>
{
    public CancelInvitationCommandValidator()
    {
        RuleFor(c => c.InvitationId).NotEmpty();
    }
}
