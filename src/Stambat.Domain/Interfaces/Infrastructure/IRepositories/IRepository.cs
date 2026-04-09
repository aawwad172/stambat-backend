using System.Linq.Expressions;

using Stambat.Domain.Entities;
using Stambat.Domain.Interfaces.Domain;

namespace Stambat.Domain.Interfaces.Infrastructure.IRepositories;

public interface IRepository<T> where T : class, IAggregateRoot
{
    Task<T?> GetByIdAsync(Guid id, QueryOptions<T>? options = null);
    Task<PaginationResult<T>> GetAllAsync(
        int? pageNumber,
        int? pageSize,
        Expression<Func<T, bool>>? filter,
        QueryOptions<T>? options = null);
    Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate,
        QueryOptions<T>? options = null);
    Task<T> AddAsync(T entity);
    T? Update(T entity);
    Task DeleteAsync(Guid id);
}
