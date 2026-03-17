using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Interfaces.Domain;
using Stambat.Domain.Interfaces.Domain.Auditing;

namespace Stambat.Domain.Entities;

/// <summary>
/// This entity represent the business that subscribe to our business.
/// </summary>
public class Tenant : IEntity, IBaseEntity
{
    public Guid Id { get; init; }
    public required string BusinessName { get; set; }
    public required string Email { get; set; }

    public Guid? TenantProfileId { get; set; }
    public TenantProfile? TenantProfile { get; set; }

    // Operational
    public bool IsActive { get; set; } = true;
    public string TimeZoneId { get; set; } = "Asia/Amman";
    public string CurrencyCode { get; set; } = "JOD";

    // Relationships
    public ICollection<UserRoleTenant> UserRoleTenants { get; set; } = [];
    public ICollection<CardTemplate> CardTemplates { get; set; } = [];

    // Auditing (from your IBaseEntity)
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
}
