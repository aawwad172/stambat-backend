using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stambat.Application.CQRS.Commands.Tenants;
using Stambat.Domain.Exceptions;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;

namespace Stambat.WebAPI.Routes.Tenants;

public class RemoveStaff : IParameterizedQueryRoute<RemoveStaffCommand>
{
    public static async Task<IResult> RegisterRoute(
        [AsParameters] RemoveStaffCommand query,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<RemoveStaffCommand> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(query);

        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

            throw new CustomValidationException("Validation failed", errors);
        }

        RemoveStaffCommandResult response = await mediator.Send(query);
        return Results.Ok(
            ApiResponse<RemoveStaffCommandResult>.SuccessResponse(response));
    }
}
