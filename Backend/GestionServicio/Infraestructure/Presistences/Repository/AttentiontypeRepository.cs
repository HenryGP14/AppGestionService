using Domain.Entities;
using Infraestructure.Presistences.Contexts;
using Infraestructure.Presistences.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Presistences.Repository
{
    internal class AttentiontypeRepository : GenericRepository<Attentiontype>, IAttentiontypeRepository
    {
        public AttentiontypeRepository(GestionServicesContext context) : base(context)
        {
        }

        public Task<Attentiontype> GetAttentiontypeByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Attentiontype>> GetListAttentiontypesAsync()
        {
            var listTypes = await GetEntityQuery(type => type.Datedelete == null)
                .AsNoTracking()
                .ToListAsync();
            return listTypes;
        }
    }
}
