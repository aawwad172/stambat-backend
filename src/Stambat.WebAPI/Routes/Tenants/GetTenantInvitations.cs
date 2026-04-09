using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stambat.Application.CQRS.Queries.Tenants;
using Stambat.Domain.Exceptions;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;

namespace Stambat.WebAPI.Routes.Tenants;

public class GetTenantInvitations : IParameterizedQueryRoute<GetTenantInvitationsQuery>
{
    public static async Task<IResult> RegisterRoute(
        [AsParameters] GetTenantInvitationsQuery query,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<GetTenantInvitationsQuery> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(query);

        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors
                .Select(e => e.ErrorMessage)
                .ToList();

            throw new CustomValidationException("Validation failed", errors);
        }

        GetTenantInvitationsQueryResult response = await mediator.Send(query);
        return Results.Ok(
            ApiResponse<GetTenantInvitationsQueryResult>.SuccessResponse(response));
    }
}
