using MediatR;

namespace Stambat.Application.CQRS.Queries.Cards;

public sealed record GetCardByIdQuery(Guid CardTemplateId) : IRequest<GetCardByIdQueryResult>;

public sealed record GetCardByIdQueryResult(CardRecord Card);
