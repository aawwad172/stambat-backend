using Stambat.Domain.Common;
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
    public int StampsRequired { get; set; } = 10; // The goal
    public string? RewardDescription { get; set; }

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
        int stampsRequired,
        string? rewardDescription,
        string? primaryColorOverride,
        string? secondaryColorOverride,
        string? logoUrlOverride,
        string? emptyStampUrl,
        string? earnedStampUrl,
        string? termsAndConditions)
    {
        Guard.AgainstDefault(tenantId, nameof(tenantId));
        Guard.AgainstNullOrEmpty(title, nameof(title));
        Guard.AgainstNegativeOrZero(stampsRequired, nameof(stampsRequired));

        return new CardTemplate
        {
            Id = IdGenerator.New(),
            TenantId = tenantId,
            Title = title,
            Description = description,
            StampsRequired = stampsRequired,
            RewardDescription = rewardDescription,
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
        int stampsRequired,
        string? rewardDescription,
        string? primaryColorOverride,
        string? secondaryColorOverride,
        string? logoUrlOverride,
        string? emptyStampUrl,
        string? earnedStampUrl,
        string? termsAndConditions,
        bool isActive)
    {
        Guard.AgainstNullOrEmpty(title, nameof(title));
        Guard.AgainstNegativeOrZero(stampsRequired, nameof(stampsRequired));

        Title = title;
        Description = description;
        StampsRequired = stampsRequired;
        RewardDescription = rewardDescription;
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
}
