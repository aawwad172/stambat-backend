using Stambat.Domain.Exceptions;

using FluentValidation;
using FluentValidation.Results;

using MediatR;
using Stambat.Application.CQRS.Commands.Users;
using Stambat.WebAPI.Models;
using Stambat.WebAPI.Interfaces;

namespace Stambat.WebAPI.Routes.Users;

public class RegisterUser : ICommandRoute<RegisterUserCommand>
{
    public static async Task<IResult> RegisterRoute(
           RegisterUserCommand command,
           IMediator mediator,
           IValidator<RegisterUserCommand> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

            // Throw a custom ValidationException that your middleware will catch
            throw new CustomValidationException("Validation failed", errors);
        }

        RegisterUserCommandResult response = await mediator.Send(command);
        return Results.Created(
            $"/users/{response.Id}",
            ApiResponse<RegisterUserCommandResult>.SuccessResponse(response));
    }
}
