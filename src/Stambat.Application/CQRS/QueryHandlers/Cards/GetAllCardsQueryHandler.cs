using System.Linq.Expressions;

using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Queries.Cards;
using Stambat.Domain.Entities;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.CQRS.QueryHandlers.Cards;

public class GetAllCardsQueryHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<GetAllCardsQueryHandler> logger,
    IUnitOfWork unitOfWork,
    IRepository<CardTemplate> cardTemplateRepository)
    : BaseHandler<GetAllCardsQuery, GetAllCardsQueryResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly IRepository<CardTemplate> _cardTemplateRepository = cardTemplateRepository;

    public override async Task<GetAllCardsQueryResult> Handle(
        GetAllCardsQuery request,
        CancellationToken cancellationToken)
    {
        Guid tenantId = _currentTenant.TenantId
            ?? throw new InvalidOperationException("TenantId should be provided via JWT claims");

        Expression<Func<CardTemplate, bool>> filter = request.IsActive.HasValue
            ? ct => ct.TenantId == tenantId && ct.IsActive == request.IsActive.Value
            : ct => ct.TenantId == tenantId;

        Expression<Func<CardTemplate, object>> orderBy = request.SortBy.Equals("Title", StringComparison.OrdinalIgnoreCase)
            ? ct => ct.Title
            : ct => ct.CreatedAt;

        PaginationResult<CardTemplate> result = await _cardTemplateRepository.GetAllAsync(
            request.Page,
            request.Size,
            filter,
            new QueryOptions<CardTemplate>
            {
                OrderBy = orderBy,
                OrderDescending = request.SortDescending
            });

        List<CardRecord> cards = [.. (result.Page ?? []).Select(ct => new CardRecord(
            Id: ct.Id,
            Title: ct.Title,
            Description: ct.Description,
            StampsRequired: ct.StampsRequired,
            RewardDescription: ct.RewardDescription,
            PrimaryColorOverride: ct.PrimaryColorOverride,
            SecondaryColorOverride: ct.SecondaryColorOverride,
            LogoUrlOverride: ct.LogoUrlOverride,
            EmptyStampUrl: ct.EmptyStampUrl,
            EarnedStampUrl: ct.EarnedStampUrl,
            TermsAndConditions: ct.TermsAndConditions,
            IsActive: ct.IsActive,
            CreatedAt: ct.CreatedAt))];

        return new GetAllCardsQueryResult(
            Cards: cards,
            TotalRecords: result.TotalRecords,
            TotalDisplayRecords: result.TotalDisplayRecords);
    }
}
