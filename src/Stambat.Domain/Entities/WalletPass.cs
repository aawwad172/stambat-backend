using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Interfaces.Domain;
using Stambat.Domain.Interfaces.Domain.Auditing;

namespace Stambat.Domain.Entities;

public class WalletPass : IEntity, ICreationAudit
{
    public Guid Id { get; init; }

    // The "Who"
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;

    // The "What"
    public Guid CardTemplateId { get; set; }
    public virtual CardTemplate CardTemplate { get; set; } = null!;

    // Progress
    public int CurrentStamps { get; set; } = 0;

    // Wallet Integration
    public string? ApplePassSerialNumber { get; set; }
    public string? GooglePayId { get; set; }

    public DateTime? LastStampedAt { get; set; }

    // Audit
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }

    // Navigation Property
    public virtual ICollection<StampTransaction> Transactions { get; set; } = [];
}
