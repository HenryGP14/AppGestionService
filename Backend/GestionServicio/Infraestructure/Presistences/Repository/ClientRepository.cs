using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;
using Infraestructure.Presistences.Contexts;
using Infraestructure.Presistences.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Presistences.Repository
{
    internal class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private readonly GestionServicesContext _context;
        public ClientRepository(GestionServicesContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            var client = await GetEntityQuery(client => client.Clientid == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return client!;
        }

        public async Task<bool> GetClientByIdentificationAsync(string identification)
        {
            var client =  GetEntityQuery(client => client.Identification == identification)
                .AsNoTracking()
                .AnyAsync();
            return await client;
        }

        public async Task<DataResponse<Client>> GetListClientsAsync(FiltersRequest request)
        {
            var response = new DataResponse<Client>();
            var clients = GetEntityQuery(client => client.Datedelete == null)
                .AsNoTracking();

            request.Sort = string.IsNullOrEmpty(request.Sort) ? "Clientid" : request.Sort;

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        clients = clients.Where(client => client.Name.Contains(request.TextFilter));
                        break;
                    case 2:
                        clients = clients.Where(client => client.Lastname!.Contains(request.TextFilter));
                        break;
                    case 3:
                        clients = clients.Where(client => client.Identification!.Contains(request.TextFilter));
                        break;
                    case 4:
                        clients = clients.Where(client => client.Address!.Contains(request.TextFilter));
                        break;
                    case 5:
                        clients = clients.Where(client => client.Email!.Contains(request.TextFilter));
                        break;
                }
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                var startDate = Convert.ToDateTime(request.StartDate);
                var endDate = Convert.ToDateTime(request.EndDate);
                clients = clients.Where(service => service.Datecreation >= startDate && service.Datecreation <= endDate);
            }

            response.TotalRecords = clients.Count();
            response.Items = await Ordering(request, clients, true).ToListAsync();
            return response;
        }
    }
}
