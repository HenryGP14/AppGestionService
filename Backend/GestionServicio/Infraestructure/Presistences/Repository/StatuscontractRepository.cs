using Domain.Entities;
using Infraestructure.Presistences.Contexts;
using Infraestructure.Presistences.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Presistences.Repository
{
    internal class StatuscontractRepository : GenericRepository<Statuscontract>, IStatuscontractRepository
    {
        public StatuscontractRepository(GestionServicesContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Statuscontract>> GetListStatuscontractsAsync()
        {
            var listStatues = await GetEntityQuery(det => det.Datedelete == null)
                .AsNoTracking()
                .ToListAsync();
            return listStatues;
        }

        public Task<Statuscontract> GetStatuscontractByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
