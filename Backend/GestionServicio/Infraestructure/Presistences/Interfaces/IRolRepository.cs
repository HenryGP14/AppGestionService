using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;

namespace Infraestructure.Presistences.Interfaces
{
    public interface IRolRepository : IGenericRepository<Rol>
    {
        Task<Rol> GetRolByIdAsync(int id);
        Task<IEnumerable<Rol>> GetListRolesAsync();
    }
}
