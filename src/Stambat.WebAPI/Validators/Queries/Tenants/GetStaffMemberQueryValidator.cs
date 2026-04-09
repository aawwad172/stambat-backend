using FluentValidation;

using Stambat.Application.CQRS.Queries.Tenants;

namespace Stambat.WebAPI.Validators.Queries.Tenants;

public class GetStaffMemberQueryValidator : AbstractValidator<GetStaffMemberQuery>
{
    public GetStaffMemberQueryValidator()
    {
        RuleFor(x => x.StaffId)
            .NotEmpty()
            .WithMessage("StaffId is required.");
    }
}
