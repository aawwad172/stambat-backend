using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stamply.Application.CQRS.Commands.Tenant;
using Stamply.Domain.Exceptions;
using Stamply.Presentation.API.Interfaces;
using Stamply.Presentation.API.Models;

namespace Stamply.Presentation.API.Routes.Tenant;

public class InviteTenant : ICommandRoute<InviteTenantCommand>
{
    public static async Task<IResult> RegisterRoute(
        [FromBody] InviteTenantCommand request,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<InviteTenantCommand> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

            // Throw a custom ValidationException that your middleware will catch
            throw new CustomValidationException("Validation failed", errors);
        }

        InviteTenantCommandResult response = await mediator.Send(request);
        return Results.Ok(
            ApiResponse<InviteTenantCommandResult>.SuccessResponse(response));
    }
}
