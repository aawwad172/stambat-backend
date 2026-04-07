using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stambat.Application.CQRS.Commands.Authentication;
using Stambat.Domain.Exceptions;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;

namespace Stambat.WebAPI.Routes.Authentication;

public class SwitchTenant : ICommandRoute<SwitchTenantCommand>
{
    public static async Task<IResult> RegisterRoute(
        [FromBody] SwitchTenantCommand request,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<SwitchTenantCommand> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

            throw new CustomValidationException("Validation failed", errors);
        }

        SwitchTenantCommandResult response = await mediator.Send(request);
        return Results.Ok(
            ApiResponse<SwitchTenantCommandResult>.SuccessResponse(response));
    }
}
