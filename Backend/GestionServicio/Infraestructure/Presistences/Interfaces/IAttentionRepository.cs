using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;

namespace Infraestructure.Presistences.Interfaces
{
    public interface IAttentionRepository: IGenericRepository<Attention>
    {
        Task<DataResponse<Attention>> GetListAttentionsByClientIdAsync(int clientId);
        Task<DataResponse<Attention>> GetListAttentionsByTurnIdAsync(int clientId);
    }
}
