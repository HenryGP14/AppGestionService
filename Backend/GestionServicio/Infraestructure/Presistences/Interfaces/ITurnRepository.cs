using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;

namespace Infraestructure.Presistences.Interfaces
{
    public interface ITurnRepository : IGenericRepository<Turn>
    {
        Task<Turn> GetTurnByIdAsync(int id);
        Task<DataResponse<TurnGestion>> GetListTurnsAsync(FiltersRequest request);
        Task<TurnGestion> GetTrunById(int turnId);
        Task<bool> CreateTurnAttention(TurnAttention request);
    }
}
