using Stambat.Domain.Common;
using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Interfaces.Domain;
using Stambat.Domain.Interfaces.Domain.Auditing;
using Stambat.Domain.ValueObjects;

namespace Stambat.Domain.Entities;

/// <summary>
/// This entity represent the business that subscribe to our business.
/// </summary>
public class Tenant : IEntity, IBaseEntity, IAggregateRoot
{
    public Guid Id { get; init; }
    public string BusinessName { get; private set; }
    public Email Email { get; private set; }

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

        return new Tenant
        {
            Id = id,
            BusinessName = businessName,
            Email = Email.Create(email),
            IsActive = true,
            IsDeleted = false
        };
    }

    public void AttachProfile(TenantProfile profile)
    {
        Guard.AgainstNull(profile, nameof(profile));
        TenantProfile = profile;
        TenantProfileId = profile.Id;
    }

    public void UpdateOperationalSettings(string timeZoneId, string currencyCode)
    {
        Guard.AgainstNullOrEmpty(timeZoneId, nameof(timeZoneId));
        Guard.AgainstNullOrEmpty(currencyCode, nameof(currencyCode));

        TimeZoneId = timeZoneId;
        CurrencyCode = currencyCode;
    }

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;

    public void AddUserRole(UserRoleTenant userRoleTenant)
    {
        Guard.AgainstNull(userRoleTenant, nameof(userRoleTenant));
        if (userRoleTenant.TenantId != Id)
        {
            throw new ArgumentException("User role tenant does not belong to this tenant.", nameof(userRoleTenant));
        }

        if (!UserRoleTenants.Any(x => x.Id == userRoleTenant.Id))
        {
            UserRoleTenants.Add(userRoleTenant);
        }
    }

    public void RemoveUserRole(UserRoleTenant userRoleTenant)
    {
        Guard.AgainstNull(userRoleTenant, nameof(userRoleTenant));
        UserRoleTenants.Remove(userRoleTenant);
    }

    public void AddCardTemplate(CardTemplate cardTemplate)
    {
        Guard.AgainstNull(cardTemplate, nameof(cardTemplate));
        if (cardTemplate.TenantId != Id)
        {
            throw new ArgumentException("Card template does not belong to this tenant.", nameof(cardTemplate));
        }

        if (!CardTemplates.Any(x => x.Id == cardTemplate.Id))
        {
            CardTemplates.Add(cardTemplate);
        }
    }

    public void RemoveCardTemplate(CardTemplate cardTemplate)
    {
        Guard.AgainstNull(cardTemplate, nameof(cardTemplate));
        CardTemplates.Remove(cardTemplate);
    }
}
