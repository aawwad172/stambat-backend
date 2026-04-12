using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stambat.Application.CQRS.Commands.Scanning;
using Stambat.Domain.Exceptions;
using Stambat.WebAPI.Interfaces;
using Stambat.WebAPI.Models;

namespace Stambat.WebAPI.Routes.Scanning;

public class ScanStamp : ICommandRoute<ScanStampCommand>
{
    public static async Task<IResult> RegisterRoute(
        [FromBody] ScanStampCommand request,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<ScanStampCommand> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors
                .Select(e => e.ErrorMessage)
                .ToList();

            throw new CustomValidationException("Validation failed", errors);
        }

        ScanStampCommandResult response = await mediator.Send(request);
        return Results.Ok(
            ApiResponse<ScanStampCommandResult>.SuccessResponse(response));
    }
}
