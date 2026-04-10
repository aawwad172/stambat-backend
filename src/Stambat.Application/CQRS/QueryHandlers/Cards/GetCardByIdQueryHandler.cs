using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Queries.Cards;
using Stambat.Domain.Entities;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.CQRS.QueryHandlers.Cards;

public class GetCardByIdQueryHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<GetCardByIdQueryHandler> logger,
    IUnitOfWork unitOfWork,
    IRepository<CardTemplate> cardTemplateRepository)
    : BaseHandler<GetCardByIdQuery, GetCardByIdQueryResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly IRepository<CardTemplate> _cardTemplateRepository = cardTemplateRepository;

    public override async Task<GetCardByIdQueryResult> Handle(
        GetCardByIdQuery request,
        CancellationToken cancellationToken)
    {
        Guid tenantId = _currentTenant.TenantId
            ?? throw new InvalidOperationException("TenantId should be provided via JWT claims");

        CardTemplate cardTemplate = await _cardTemplateRepository.FirstOrDefaultAsync(
            ct => ct.Id == request.CardTemplateId && ct.TenantId == tenantId)
            ?? throw new NotFoundException($"Card template: {request.CardTemplateId} was not found");

        CardRecord card = new(
            Id: cardTemplate.Id,
            Title: cardTemplate.Title,
            Description: cardTemplate.Description,
            StampsRequired: cardTemplate.StampsRequired,
            RewardDescription: cardTemplate.RewardDescription,
            PrimaryColorOverride: cardTemplate.PrimaryColorOverride,
            SecondaryColorOverride: cardTemplate.SecondaryColorOverride,
            LogoUrlOverride: cardTemplate.LogoUrlOverride,
            EmptyStampUrl: cardTemplate.EmptyStampUrl,
            EarnedStampUrl: cardTemplate.EarnedStampUrl,
            TermsAndConditions: cardTemplate.TermsAndConditions,
            JoinUrl: cardTemplate.JoinUrl,
            JoinQrCodeBase64: cardTemplate.JoinQrCodeBase64,
            IsActive: cardTemplate.IsActive,
            CreatedAt: cardTemplate.CreatedAt);

        return new GetCardByIdQueryResult(card);
    }
}
