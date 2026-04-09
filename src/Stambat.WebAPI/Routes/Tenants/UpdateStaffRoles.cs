using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stambat.Application.CQRS.Commands.Tenants;
using Stambat.Domain.Exceptions;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;

namespace Stambat.WebAPI.Routes.Tenants;

public class UpdateStaffRoles : ICommandRoute<UpdateStaffRolesCommand>
{
    public static async Task<IResult> RegisterRoute(
        [FromBody] UpdateStaffRolesCommand request,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<UpdateStaffRolesCommand> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

            throw new CustomValidationException("Validation failed", errors);
        }

        UpdateStaffRolesCommandResult response = await mediator.Send(request);
        return Results.Ok(
            ApiResponse<UpdateStaffRolesCommandResult>.SuccessResponse(response));
    }
}
