using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;
using Infraestructure.Presistences.Contexts;
using Infraestructure.Presistences.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Presistences.Repository
{
    internal class AttentionstatusRepository : GenericRepository<Attentionstatus>, IAttentionstatusRepository
    {
        public AttentionstatusRepository(GestionServicesContext context) : base(context)
        {
        }

        public Task<Attentionstatus> GetAttentionsStatusByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Attentionstatus>> GetListAttentionsStatusAsync()
        {
            var listStatus = await GetEntityQuery(state => state.Datedelete == null)
                .AsNoTracking()
                .ToListAsync();
            return listStatus;
        }
    }
}
