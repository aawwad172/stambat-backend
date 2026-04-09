using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stambat.Application.CQRS.Queries.Tenants;
using Stambat.Domain.Exceptions;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;

namespace Stambat.WebAPI.Routes.Tenants;

public class GetStaffMember : IParameterizedQueryRoute<GetStaffMemberQuery>
{
    public static async Task<IResult> RegisterRoute(
        [AsParameters] GetStaffMemberQuery query,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<GetStaffMemberQuery> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(query);

        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

            throw new CustomValidationException("Validation failed", errors);
        }

        GetStaffMemberQueryResult response = await mediator.Send(query);
        return Results.Ok(
            ApiResponse<StaffRecord>.SuccessResponse(response.Staff));
    }
}
