using MediatR;

using Microsoft.Extensions.Logging;

using Stambat.Application.Services;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.CQRS;

public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected readonly ICurrentUserService _currentUser;
    protected readonly ITenantProviderService _currentTenant;
    protected readonly ILogger _logger;
    protected readonly IUnitOfWork _unitOfWork;

    protected BaseHandler(ICurrentUserService currentUserService, ITenantProviderService currentTenantProviderService, ILogger logger, IUnitOfWork unitOfWork)
    {
        _currentUser = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));

        _currentTenant = currentTenantProviderService ?? throw new ArgumentNullException(nameof(currentTenantProviderService));

        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    // Make this abstract to force derived handlers implement it,
    // or virtual if you want a default behavior.
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
