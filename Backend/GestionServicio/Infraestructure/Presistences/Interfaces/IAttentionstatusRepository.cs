using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;

namespace Infraestructure.Presistences.Interfaces
{
    public interface IAttentionstatusRepository : IGenericRepository<Attentionstatus>
    {
        Task<Attentionstatus> GetAttentionsStatusByIdAsync(int id);
        Task<IEnumerable<Attentionstatus>> GetListAttentionsStatusAsync();
    }
}
