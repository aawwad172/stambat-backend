using Microsoft.Extensions.Logging;

using Stamply.Application.CQRS.Queries.Users;
using Stamply.Domain.Entities.Identity;
using Stamply.Domain.Exceptions;
using Stamply.Domain.Interfaces.Application.Services;
using Stamply.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stamply.Application.CQRS.QueryHandlers.Users;

public class IsUserVerifiedQueryHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<IsUserVerifiedQueryHandler> logger,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository)
    : BaseHandler<IsUserVerifiedQuery, IsUserVerifiedQueryResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly IUserRepository _userRepository = userRepository;
    public override async Task<IsUserVerifiedQueryResult> Handle(IsUserVerifiedQuery request, CancellationToken cancellationToken)
    {
        try
        {
            User user = await _userRepository.GetByIdAsync(request.UserId)
           ?? throw new NotFoundException($"User with Id: {request.UserId} is not found");

            return new IsUserVerifiedQueryResult(IsVerified: user.IsVerified);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred during verifying email: {Message}", ex.Message);
            await _unitOfWork.RollbackAsync();
            throw;
        }

    }
}
