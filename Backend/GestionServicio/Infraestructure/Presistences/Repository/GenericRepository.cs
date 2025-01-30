using Infraestructure.Commons.Request;
using Infraestructure.Presistences.Contexts;
using Infraestructure.Presistences.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using Infraestructure.Helpers;

namespace Infraestructure.Presistences.Repository
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly GestionServicesContext _context;
        private readonly DbSet<T> _entity;

        public GenericRepository(GestionServicesContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public async Task<bool> SaveAsync(T entity)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _context.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _context.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        public IQueryable<T> GetEntityQuery(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _entity;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query;
        }

        public IQueryable<R> Ordering<R>(PaginationRequest request, IQueryable<R> queryable, bool pagination = false) where R : class
        {
            IQueryable<R> queryDto = request.Order == "desc" ? queryable.OrderBy($"{request.Sort} descending") : queryable.OrderBy($"{request.Sort} ascending");
            if (pagination) queryDto = queryDto.Paginate(request);
            return queryDto;
        }
    }
}
