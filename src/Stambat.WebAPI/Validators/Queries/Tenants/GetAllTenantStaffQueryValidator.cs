using FluentValidation;

using Stambat.Application.CQRS.Queries.Tenants;

namespace Stambat.WebAPI.Validators.Queries.Tenants;

public class GetAllTenantStaffQueryValidator : AbstractValidator<GetAllTenantStaffQuery>
{
    public GetAllTenantStaffQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);

        RuleFor(x => x.Size)
            .InclusiveBetween(1, 100);
    }
}
