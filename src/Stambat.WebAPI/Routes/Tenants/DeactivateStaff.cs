using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stambat.Application.CQRS.Commands.Tenants;
using Stambat.Domain.Exceptions;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;

namespace Stambat.WebAPI.Routes.Tenants;

public class DeactivateStaff : IParameterizedQueryRoute<DeactivateStaffCommand>
{
    public static async Task<IResult> RegisterRoute(
        [AsParameters] DeactivateStaffCommand query,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<DeactivateStaffCommand> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(query);

        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

            throw new CustomValidationException("Validation failed", errors);
        }

        DeactivateStaffCommandResult response = await mediator.Send(query);
        return Results.Ok(
            ApiResponse<DeactivateStaffCommandResult>.SuccessResponse(response));
    }
}
