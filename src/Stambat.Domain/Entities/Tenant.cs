using Stambat.Domain.Common;
using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Interfaces.Domain;
using Stambat.Domain.Interfaces.Domain.Auditing;

namespace Stambat.Domain.Entities;

/// <summary>
/// This entity represent the business that subscribe to our business.
/// </summary>
public class Tenant : IEntity, IBaseEntity, IAggregateRoot
{
    public Guid Id { get; init; }
    public string BusinessName { get; private set; }
    public string Email { get; private set; }

    public Guid? TenantProfileId { get; private set; }
    public TenantProfile? TenantProfile { get; private set; }

    // Operational
    public bool IsActive { get; private set; } = true;
    public string TimeZoneId { get; private set; } = "Asia/Amman";
    public string CurrencyCode { get; private set; } = "JOD";

    // Relationships
    public ICollection<UserRoleTenant> UserRoleTenants { get; private set; } = [];
    public ICollection<CardTemplate> CardTemplates { get; private set; } = [];

    // Auditing
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }

    // EF Core constructor
    private Tenant()
    {
        BusinessName = default!;
        Email = default!;
    }

    public static Tenant Create(
        Guid id,
        string businessName,
        string email)
    {
        Guard.AgainstDefault(id, nameof(id));
        Guard.AgainstNullOrEmpty(businessName, nameof(businessName));
        Guard.AgainstNullOrEmpty(email, nameof(email));

        return new Tenant
        {
            Id = id,
            BusinessName = businessName,
            Email = email,
            IsActive = true,
            IsDeleted = false
        };
    }
}
