using System.Linq.Expressions;

namespace Stambat.Domain.Interfaces.Infrastructure.IRepositories;

/// <summary>
/// Optional query modifiers for generic repository methods.
/// Pass navigation property expressions to eagerly load related data,
/// and an optional ordering expression.
/// </summary>
public class QueryOptions<T> where T : class
{
    /// <summary>
    /// Navigation properties to eagerly load via .Include().
    /// Example: new Expression&lt;Func&lt;T, object&gt;&gt;[] { t => t.Roles, t => t.Profile }
    /// </summary>
    public IReadOnlyList<Expression<Func<T, object>>>? Includes { get; init; }

    /// <summary>
    /// Optional ordering expression. Ascending by default.
    /// Example: t => t.CreatedAt
    /// </summary>
    public Expression<Func<T, object>>? OrderBy { get; init; }

    /// <summary>
    /// If true, order descending instead of ascending.
    /// </summary>
    public bool OrderDescending { get; init; } = false;

    /// <summary>
    /// If true, use .AsNoTracking() for read-only queries.
    /// Defaults to false to preserve current behavior.
    /// </summary>
    public bool AsNoTracking { get; init; } = false;
}
