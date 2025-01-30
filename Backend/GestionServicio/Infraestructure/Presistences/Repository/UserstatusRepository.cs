using Domain.Entities;
using Infraestructure.Presistences.Contexts;
using Infraestructure.Presistences.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Presistences.Repository
{
    internal class UserstatusRepository : GenericRepository<Userstatus>, IUserstatusRepository
    {
        public UserstatusRepository(GestionServicesContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Userstatus>> GetListUserstatussAsync()
        {
            var listUserstatuses = await GetEntityQuery(det => det.Datedelete == null)
                .AsNoTracking()
                .ToListAsync();
            return listUserstatuses;
        }

        public Task<Userstatus> GetUserstatusByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
