namespace Stamply.Domain.Interfaces.Application.Services;

public interface ITenantProviderService
{
    Guid? TenantId { get; set; }
}
