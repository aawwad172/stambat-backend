using MediatR;

namespace Stambat.Application.CQRS.Commands.Cards;

public sealed record CreateCardCommand(
    string Title,
    string? Description,
    int StampsRequired,
    string? RewardDescription,
    string? PrimaryColorOverride,
    string? SecondaryColorOverride,
    string? LogoUrlOverride,
    string? EmptyStampUrl,
    string? EarnedStampUrl,
    string? TermsAndConditions) : IRequest<CreateCardCommandResult>;

public sealed record CreateCardCommandResult(Guid CardTemplateId);
