using System;

using Stamply.Domain.Interfaces.Application.Services;

namespace Stamply.Application.Services;

public class TenantProviderService : ITenantProviderService
{
    public Guid? TenantId { get; set; }
}
