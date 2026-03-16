using MediatR;

namespace Stamply.Application.CQRS.Queries.Users;

public sealed record IsUserVerifiedQuery(Guid UserId) : IRequest<IsUserVerifiedQueryResult>;

public sealed record IsUserVerifiedQueryResult(bool IsVerified);
