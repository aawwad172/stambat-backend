using Microsoft.EntityFrameworkCore;

using Stambat.Domain.Entities;

namespace Stambat.Infrastructure.Pagination;

public static class QueryableExtensions
{
    // This method is an extension method that extends the IQueryable interface.
    // The default behavior of this method is to return everything from the query.
    public static async Task<PaginationResult<T>> ToPagedQueryAsync<T>(this IQueryable<T> query, int? pageNumber, int? pageSize)
    {
        int totalRecords = await query.CountAsync();

        if (pageNumber is not null && pageSize is not null)
            query = query.Skip((pageNumber.Value - 1) * pageSize.Value);

        if (pageSize is not null)
            query = query.Take(pageSize.Value);

        List<T> result = await query.ToListAsync();

        return new PaginationResult<T>
        {
            Page = result,
            TotalRecords = totalRecords,
            TotalDisplayRecords = result.Count
        };
    }
}
