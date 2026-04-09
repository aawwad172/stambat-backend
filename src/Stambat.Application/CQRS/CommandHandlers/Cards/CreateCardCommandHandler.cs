using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Cards;
using Stambat.Domain.Entities;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.CQRS.CommandHandlers.Cards;

public class CreateCardCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<CreateCardCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IRepository<CardTemplate> cardTemplateRepository,
    IRepository<Tenant> tenantRepository)
    : BaseHandler<CreateCardCommand, CreateCardCommandResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly IRepository<CardTemplate> _cardTemplateRepository = cardTemplateRepository;
    private readonly IRepository<Tenant> _tenantRepository = tenantRepository;

    public override async Task<CreateCardCommandResult> Handle(
        CreateCardCommand request,
        CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            Guid tenantId = _currentTenant.TenantId
                ?? throw new ArgumentException("TenantId should be provided via JWT claims.");

            _ = await _tenantRepository.GetByIdAsync(tenantId)
                ?? throw new NotFoundException($"Tenant: {tenantId} was not found");

            CardTemplate? existing = await _cardTemplateRepository.FirstOrDefaultAsync(
                ct => ct.TenantId == tenantId && ct.Title == request.Title);

            if (existing is not null)
                throw new ConflictException($"A card template with the title '{request.Title}' already exists for this tenant.");

            CardTemplate cardTemplate = CardTemplate.Create(
                tenantId: tenantId,
                title: request.Title,
                description: request.Description,
                stampsRequired: request.StampsRequired,
                rewardDescription: request.RewardDescription,
                primaryColorOverride: request.PrimaryColorOverride,
                secondaryColorOverride: request.SecondaryColorOverride,
                logoUrlOverride: request.LogoUrlOverride,
                emptyStampUrl: request.EmptyStampUrl,
                earnedStampUrl: request.EarnedStampUrl,
                termsAndConditions: request.TermsAndConditions);

            await _cardTemplateRepository.AddAsync(cardTemplate);

            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new CreateCardCommandResult(cardTemplate.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating card template: {Message}", ex.Message);
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
