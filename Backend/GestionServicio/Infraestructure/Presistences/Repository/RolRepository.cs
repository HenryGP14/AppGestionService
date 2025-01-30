using Domain.Entities;
using Infraestructure.Commons.Request;
using Infraestructure.Presistences.Contexts;
using Infraestructure.Presistences.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Presistences.Repository
{
    internal class RolRepository : GenericRepository<Rol>, IRolRepository
    {
        public RolRepository(GestionServicesContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Rol>> GetListRolesAsync()
        {
            var listRols = await GetEntityQuery(det => det.Datedelete == null)
                .AsNoTracking()
                .ToListAsync();
            return listRols;
        }

        public Task<Rol> GetRolByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
