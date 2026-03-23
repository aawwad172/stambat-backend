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

    // Status
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; } = false;

    // Navigation
    public virtual ICollection<WalletPass> IssuedPasses { get; set; } = [];
}
