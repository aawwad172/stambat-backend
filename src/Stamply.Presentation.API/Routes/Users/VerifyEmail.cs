using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stamply.Application.CQRS.Commands.Users;
using Stamply.Domain.Exceptions;
using Stamply.Presentation.API.Interfaces;
using Stamply.Presentation.API.Models;

namespace Stamply.Presentation.API.Routes.Users;

public class VerifyEmail : ICommandRoute<VerifyEmailCommand>
{
    public static async Task<IResult> RegisterRoute(
        [FromBody] VerifyEmailCommand request,
        [FromServices] IMediator mediator,
        [FromServices] FluentValidation.IValidator<VerifyEmailCommand> validator)
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
        // The command will contain the Token (and potentially the UserId)
        VerifyEmailCommandResult result = await mediator.Send(request);

        return Results.Ok(ApiResponse<VerifyEmailCommandResult>.SuccessResponse(result));
    }
}
