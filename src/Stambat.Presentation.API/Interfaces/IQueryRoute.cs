using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Stambat.WebAPI.Interfaces;

public interface IQueryRoute<TQuery> where TQuery : notnull
{
    static abstract Task<IResult> RegisterRoute(
    [AsParameters] TQuery query,
    [FromServices] IMediator mediator);
}
