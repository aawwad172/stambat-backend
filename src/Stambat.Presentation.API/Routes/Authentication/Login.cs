
using Stambat.Application.CQRS.Commands.Authentication;
using Stambat.Domain.Exceptions;

using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Stambat.WebAPI.Models;
using Stambat.WebAPI.Interfaces;

namespace Stambat.WebAPI.Routes.Authentication;

public class Login : ICommandRoute<LoginCommand>
{
    public static async Task<IResult> RegisterRoute(
        [FromBody] LoginCommand request,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<LoginCommand> validator)
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

        LoginCommandResult response = await mediator.Send(request);
        return Results.Ok(
            ApiResponse<LoginCommandResult>.SuccessResponse(response));
    }
}
