using Stambat.Domain.Common;
using Stambat.Domain.Enums;
using Stambat.Domain.Interfaces.Domain;
using Stambat.Domain.Interfaces.Domain.Auditing;

namespace Stambat.Domain.Entities;

public class CardTemplate : IBaseEntity, IAggregateRoot
{
    public Guid Id { get; init; }

    // Which business owns this card?
    public required Guid TenantId { get; set; }
    public virtual Tenant Tenant { get; set; } = null!;

    // Basic Info
    public required string Title { get; set; } // e.g., "Latte Loyalty"
    public string? Description { get; set; } // e.g., "Valid for all large drinks"

    // The "Rules"
    public decimal RequiredBalance { get; set; } = 10; // The goal (stamps count or points total)
    public string? RewardDescription { get; set; }
    public CardType CardType { get; set; } = CardType.Standard;
    public int? ExpiryDurationInDays { get; set; }
    public RedemptionType RedemptionType { get; set; } = RedemptionType.Stamps;
    public decimal? PointsPerCurrencyUnit { get; set; } // Conversion rate for Points cards (e.g., 10 = 1 JOD → 10 points)

    // Branding Overrides (Optional - defaults to Tenant colors if null)
    public string? PrimaryColorOverride { get; set; }
    public string? SecondaryColorOverride { get; set; }

    // Logo for the card, if empty will use the logo of the tenant in the tenant profile if available
    public string? LogoUrlOverride { get; set; }
    public string? EmptyStampUrl { get; set; }
    public string? EarnedStampUrl { get; set; }
    public string? TermsAndConditions { get; set; }

    // Customer Onboarding
    public string? JoinUrl { get; private set; }
    public string? JoinQrCodeBase64 { get; private set; }

    // Wallet Integration (Google Loyalty Class ID, Apple Pass Type ID, etc.)
    public string? WalletClassId { get; private set; }

    // Status
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; } = false;

    // Navigation
    public virtual ICollection<WalletPass> IssuedPasses { get; set; } = [];

    public static CardTemplate Create(
        Guid tenantId,
        string title,
        string? description,
        decimal requiredBalance,
        string? rewardDescription,
        CardType cardType,
        int? expiryDurationInDays,
        RedemptionType redemptionType,
        decimal? pointsPerCurrencyUnit,
        string? primaryColorOverride,
        string? secondaryColorOverride,
        string? logoUrlOverride,
        string? emptyStampUrl,
        string? earnedStampUrl,
        string? termsAndConditions)
    {
        Guard.AgainstDefault(tenantId, nameof(tenantId));
        Guard.AgainstNullOrEmpty(title, nameof(title));
        Guard.AgainstNegativeOrZero(requiredBalance, nameof(requiredBalance));
        ValidateExpiryRules(cardType, expiryDurationInDays);
        ValidateRedemptionRules(redemptionType, pointsPerCurrencyUnit, requiredBalance);

        return new CardTemplate
        {
            Id = IdGenerator.New(),
            TenantId = tenantId,
            Title = title,
            Description = description,
            RequiredBalance = requiredBalance,
            RewardDescription = rewardDescription,
            CardType = cardType,
            ExpiryDurationInDays = expiryDurationInDays,
            RedemptionType = redemptionType,
            PointsPerCurrencyUnit = pointsPerCurrencyUnit,
            PrimaryColorOverride = primaryColorOverride,
            SecondaryColorOverride = secondaryColorOverride,
            LogoUrlOverride = logoUrlOverride,
            EmptyStampUrl = emptyStampUrl,
            EarnedStampUrl = earnedStampUrl,
            TermsAndConditions = termsAndConditions
        };
    }

    public void Update(
        string title,
        string? description,
        decimal requiredBalance,
        string? rewardDescription,
        CardType cardType,
        int? expiryDurationInDays,
        RedemptionType redemptionType,
        decimal? pointsPerCurrencyUnit,
        string? primaryColorOverride,
        string? secondaryColorOverride,
        string? logoUrlOverride,
        string? emptyStampUrl,
        string? earnedStampUrl,
        string? termsAndConditions,
        bool isActive)
    {
        Guard.AgainstNullOrEmpty(title, nameof(title));
        Guard.AgainstNegativeOrZero(requiredBalance, nameof(requiredBalance));
        ValidateExpiryRules(cardType, expiryDurationInDays);
        ValidateRedemptionRules(redemptionType, pointsPerCurrencyUnit, requiredBalance);

        Title = title;
        Description = description;
        RequiredBalance = requiredBalance;
        RewardDescription = rewardDescription;
        CardType = cardType;
        ExpiryDurationInDays = expiryDurationInDays;
        RedemptionType = redemptionType;
        PointsPerCurrencyUnit = pointsPerCurrencyUnit;
        PrimaryColorOverride = primaryColorOverride;
        SecondaryColorOverride = secondaryColorOverride;
        LogoUrlOverride = logoUrlOverride;
        EmptyStampUrl = emptyStampUrl;
        EarnedStampUrl = earnedStampUrl;
        TermsAndConditions = termsAndConditions;
        IsActive = isActive;
    }

    public void SetJoinInfo(string joinUrl, string joinQrCodeBase64)
    {
        Guard.AgainstNullOrEmpty(joinUrl, nameof(joinUrl));
        Guard.AgainstNullOrEmpty(joinQrCodeBase64, nameof(joinQrCodeBase64));

        JoinUrl = joinUrl;
        JoinQrCodeBase64 = joinQrCodeBase64;
    }

    public void SetWalletClassId(string walletClassId)
    {
        Guard.AgainstNullOrEmpty(walletClassId, nameof(walletClassId));
        WalletClassId = walletClassId;
    }

    public void SoftDelete()
    {
        IsDeleted = true;
        IsActive = false;
    }

    private static void ValidateExpiryRules(CardType cardType, int? expiryDurationInDays)
    {
        if (cardType == CardType.Expirable)
        {
            if (expiryDurationInDays is null)
                throw new ArgumentException("ExpiryDurationInDays is required for Expirable cards.", nameof(expiryDurationInDays));

            Guard.AgainstNegativeOrZero(expiryDurationInDays.Value, nameof(expiryDurationInDays));
        }
        else
        {
            if (expiryDurationInDays is not null)
                throw new ArgumentException("ExpiryDurationInDays must be null for Standard cards.", nameof(expiryDurationInDays));
        }
    }

    private static void ValidateRedemptionRules(RedemptionType redemptionType, decimal? pointsPerCurrencyUnit, decimal requiredBalance)
    {
        if (redemptionType == RedemptionType.Points)
        {
            if (pointsPerCurrencyUnit is null)
                throw new ArgumentException("PointsPerCurrencyUnit is required for Points cards.", nameof(pointsPerCurrencyUnit));

            Guard.AgainstNegativeOrZero(pointsPerCurrencyUnit.Value, nameof(pointsPerCurrencyUnit));
        }
        else
        {
            if (pointsPerCurrencyUnit is not null)
                throw new ArgumentException("PointsPerCurrencyUnit must be null for Stamps cards.", nameof(pointsPerCurrencyUnit));

            if (requiredBalance != Math.Floor(requiredBalance))
                throw new ArgumentException("RequiredBalance must be a whole number for Stamps cards.", nameof(requiredBalance));
        }
    }
}
