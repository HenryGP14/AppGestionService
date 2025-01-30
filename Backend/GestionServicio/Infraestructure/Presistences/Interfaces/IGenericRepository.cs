using Infraestructure.Commons.Request;
using System.Linq.Expressions;

namespace Infraestructure.Presistences.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> SaveAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        IQueryable<T> GetEntityQuery(Expression<Func<T, bool>>? filter = null);
        IQueryable<R> Ordering<R>(PaginationRequest request, IQueryable<R> queryable, bool pagination = false) where R : class;
    }
}
