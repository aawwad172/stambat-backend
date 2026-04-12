using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stambat.Application.CQRS.Commands.Wallet;
using Stambat.Domain.Exceptions;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;

namespace Stambat.WebAPI.Routes.Wallet;

public class CustomerOnboard : ICommandRoute<CustomerOnboardCommand>
{
    public static async Task<IResult> RegisterRoute(
        [FromBody] CustomerOnboardCommand request,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<CustomerOnboardCommand> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors
                .Select(e => e.ErrorMessage)
                .ToList();

            throw new CustomValidationException("Validation failed", errors);
        }

        CustomerOnboardCommandResult response = await mediator.Send(request);
        return Results.Ok(
            ApiResponse<CustomerOnboardCommandResult>.SuccessResponse(response));
    }
}
