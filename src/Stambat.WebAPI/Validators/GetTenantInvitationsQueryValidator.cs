using FluentValidation;

using Stambat.Application.CQRS.Queries.Tenants;

namespace Stambat.WebAPI.Validators;

public class GetTenantInvitationsQueryValidator : AbstractValidator<GetTenantInvitationsQuery>
{
    public GetTenantInvitationsQueryValidator()
    {
        RuleFor(q => q.Page).GreaterThan(0);
        RuleFor(q => q.Size).InclusiveBetween(1, 100);
    }
}
