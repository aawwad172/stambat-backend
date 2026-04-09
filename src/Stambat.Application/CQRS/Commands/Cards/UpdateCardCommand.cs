using MediatR;

namespace Stambat.Application.CQRS.Commands.Cards;

public sealed record UpdateCardCommand(
    Guid CardTemplateId,
    string Title,
    string? Description,
    int StampsRequired,
    string? RewardDescription,
    string? PrimaryColorOverride,
    string? SecondaryColorOverride,
    string? LogoUrlOverride,
    string? EmptyStampUrl,
    string? EarnedStampUrl,
    string? TermsAndConditions,
    bool IsActive) : IRequest<UpdateCardCommandResult>;

public sealed record UpdateCardCommandResult(string Message);
