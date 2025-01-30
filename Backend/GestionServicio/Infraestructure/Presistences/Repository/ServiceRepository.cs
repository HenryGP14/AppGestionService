using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;
using Infraestructure.Presistences.Contexts;
using Infraestructure.Presistences.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Presistences.Repository
{
    internal class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        private readonly GestionServicesContext _context;
        public ServiceRepository(GestionServicesContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DataResponse<Service>> GetListServcesAsync(FiltersRequest request)
        {
            var response = new DataResponse<Service>();
            var services = GetEntityQuery(service => service.Datedelete == null)
                .AsNoTracking();

            request.Sort = string.IsNullOrEmpty(request.Sort) ? "Serviceid" : request.Sort;

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        services = services.Where(service => service.Servicename.Contains(request.TextFilter));
                        break;
                    case 2:
                        services = services.Where(service => service.Servicedescription!.Contains(request.TextFilter));
                        break;
                }
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                var startDate = Convert.ToDateTime(request.StartDate);
                var endDate = Convert.ToDateTime(request.EndDate);
                services = services.Where(service => service.Datecreation >= startDate && service.Datecreation <= endDate);
            }

            response.TotalRecords = services.Count();
            response.Items = await Ordering(request, services, true).ToListAsync();
            return response;
        }

        public Task<Service> GetServiceByIdAsync(int id)
        {
            var response = GetEntityQuery(service => service.Serviceid == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return response!;
        }
    }
}
