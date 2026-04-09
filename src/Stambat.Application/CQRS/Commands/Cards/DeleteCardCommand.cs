using MediatR;

namespace Stambat.Application.CQRS.Commands.Cards;

public sealed record DeleteCardCommand(Guid CardTemplateId) : IRequest<DeleteCardCommandResult>;

public sealed record DeleteCardCommandResult(string Message);
