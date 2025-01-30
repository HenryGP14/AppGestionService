using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;
using Infraestructure.Presistences.Contexts;
using Infraestructure.Presistences.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Presistences.Repository
{
    internal class CashRepository : GenericRepository<Cash>, ICashRepository
    {
        private readonly GestionServicesContext _context;
        public CashRepository(GestionServicesContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Cash> GetCashByIdAsync(int id)
        {
            var response = await GetEntityQuery(cash => cash.Cashid == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return response!;
        }
        public async Task<DataResponse<Cash>> GetListCashesAsync(FiltersRequest request)
        {
            var response = new DataResponse<Cash>();
            var cashes = GetEntityQuery(cash => cash.Datedelete == null)
                .AsNoTracking();

            request.Sort = string.IsNullOrEmpty(request.Sort) ? "Cashid" : request.Sort;

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        cashes = cashes.Where(cash => cash.Cashdescription.Contains(request.TextFilter));
                        break;
                    case 2:
                        cashes = cashes.Where(cash => cash.Active.Contains(request.TextFilter));
                        break;
                }
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                var startDate = Convert.ToDateTime(request.StartDate);
                var endDate = Convert.ToDateTime(request.EndDate);
                cashes = cashes.Where(service => service.Datecreation >= startDate && service.Datecreation <= endDate);
            }

            response.TotalRecords = cashes.Count();
            response.Items = await Ordering(request, cashes, true).ToListAsync();
            return response;
        }
    }
}
