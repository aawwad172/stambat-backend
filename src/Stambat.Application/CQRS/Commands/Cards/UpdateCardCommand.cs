using MediatR;

using Stambat.Domain.Enums;

namespace Stambat.Application.CQRS.Commands.Cards;

public sealed record UpdateCardCommand(
    Guid CardTemplateId,
    string Title,
    string? Description,
    decimal RequiredBalance,
    string? RewardDescription,
    CardType CardType,
    int? ExpiryDurationInDays,
    RedemptionType RedemptionType,
    decimal? PointsPerCurrencyUnit,
    string? PrimaryColorOverride,
    string? SecondaryColorOverride,
    string? LogoUrlOverride,
    string? EmptyStampUrl,
    string? EarnedStampUrl,
    string? TermsAndConditions,
    bool IsActive) : IRequest<UpdateCardCommandResult>;

public sealed record UpdateCardCommandResult(string Message);
