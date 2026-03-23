using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stambat.Application.CQRS.Queries.Tenants;
using Stambat.Domain.Exceptions;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;

namespace Stambat.WebAPI.Routes.Tenants;

public class GetAllTenantStaff : IParameterizedQueryRoute<GetAllTenantStaffQuery>
{
    public static async Task<IResult> RegisterRoute(
        [AsParameters] GetAllTenantStaffQuery query,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<GetAllTenantStaffQuery> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(query);

        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

            // Throw a custom ValidationException that your middleware will catch
            throw new CustomValidationException("Validation failed", errors);
        }

        GetAllTenantStaffQueryResult response = await mediator.Send(query);
        return Results.Ok(
            ApiResponse<IEnumerable<StaffRecord>>.SuccessResponse(response.Staff));
    }
}
