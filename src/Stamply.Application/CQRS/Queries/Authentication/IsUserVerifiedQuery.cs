using MediatR;

namespace Stamply.Application.CQRS.Queries.Authentication;

public sealed record IsUserVerifiedQuery(Guid UserId) : IRequest<IsUserVerifiedQueryResult>;

public sealed record IsUserVerifiedQueryResult(bool IsVerified);
