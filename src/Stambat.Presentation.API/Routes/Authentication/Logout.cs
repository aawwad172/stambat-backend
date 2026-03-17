using Stambat.Application.CQRS.Commands.Authentication;
using Stambat.Domain.Exceptions;

using FluentValidation;
using FluentValidation.Results;

using MediatR;
using Stambat.WebAPI.Models;
using Stambat.WebAPI.Interfaces;

namespace Stambat.WebAPI.Routes.Authentication;

public class Logout : ICommandRoute<LogoutCommand>
{
    public static async Task<IResult> RegisterRoute(
        LogoutCommand command,
        IMediator mediator,
        IValidator<LogoutCommand> validator)
    {
        ValidationResult? validationResult = await validator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

            // Throw a custom ValidationException that your middleware will catch
            throw new CustomValidationException("Validation failed", errors);
        }

        LogoutCommandResult response = await mediator.Send(command);

        return Results.Ok(ApiResponse<LogoutCommandResult>.SuccessResponse(response));
    }
}
