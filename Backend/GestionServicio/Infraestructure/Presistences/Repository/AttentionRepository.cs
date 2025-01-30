using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;
using Infraestructure.Presistences.Contexts;
using Infraestructure.Presistences.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Presistences.Repository
{
    internal class AttentionRepository : GenericRepository<Attention>, IAttentionRepository
    {
        private readonly GestionServicesContext _context;
        public AttentionRepository(GestionServicesContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DataResponse<Attention>> GetListAttentionsByClientIdAsync(int clientId)
        {
            var response = new DataResponse<Attention>();
            var request = new FiltersRequest();
            request.Sort = "Datecreation";
            var attentions = GetEntityQuery(attention => attention.ClientClientid == clientId && attention.Datedelete == null)
                .AsNoTracking();
            response.TotalRecords = await attentions.CountAsync();
            response.Items = await Ordering(request, attentions, true).ToListAsync();
            return response;
        }

        public async Task<DataResponse<Attention>> GetListAttentionsByTurnIdAsync(int turnId)
        {
            var response = new DataResponse<Attention>();
            var request = new FiltersRequest();
            request.Sort = "Datecreation";
            var attentions = GetEntityQuery(attention => attention.TurnTurnid == turnId && attention.Datedelete == null)
                .AsNoTracking();
            response.TotalRecords = await attentions.CountAsync();
            response.Items = await Ordering(request, attentions, true).ToListAsync();
            return response;
        }
    }
}
