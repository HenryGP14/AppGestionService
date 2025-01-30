using Infraestructure.Commons.Request;

namespace Infraestructure.Helpers
{
    public static class QueryableHelpers
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationRequest request)
        {
            return queryable.Skip((request.NumPage - 1) * request.Records).Take(request.Records);
        }
    }
}
