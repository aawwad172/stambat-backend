using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stambat.Application.CQRS.Commands.Scanning;
using Stambat.Domain.Exceptions;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;

namespace Stambat.WebAPI.Routes.Scanning;

public class ScanRedeem : ICommandRoute<ScanRedeemCommand>
{
    public static async Task<IResult> RegisterRoute(
        [FromBody] ScanRedeemCommand request,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<ScanRedeemCommand> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors
                .Select(e => e.ErrorMessage)
                .ToList();

            throw new CustomValidationException("Validation failed", errors);
        }

        ScanRedeemCommandResult response = await mediator.Send(request);
        return Results.Ok(
            ApiResponse<ScanRedeemCommandResult>.SuccessResponse(response));
    }
}
