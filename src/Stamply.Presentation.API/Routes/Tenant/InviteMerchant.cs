using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stamply.Application.CQRS.Commands.Tenant;
using Stamply.Domain.Exceptions;
using Stamply.Presentation.API.Interfaces;
using Stamply.Presentation.API.Models;

namespace Stamply.Presentation.API.Routes.Tenant;

public class InviteMerchant : ICommandRoute<InviteMerchantCommand>
{
    public static async Task<IResult> RegisterRoute(
        [FromBody] InviteMerchantCommand request,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<InviteMerchantCommand> validator)
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

        InviteMerchantCommandResult response = await mediator.Send(request);
        return Results.Ok(
            ApiResponse<InviteMerchantCommandResult>.SuccessResponse(response));
    }
}
