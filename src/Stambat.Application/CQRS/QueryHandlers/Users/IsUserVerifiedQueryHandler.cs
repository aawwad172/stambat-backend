using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Queries.Users;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.CQRS.QueryHandlers.Users;

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
