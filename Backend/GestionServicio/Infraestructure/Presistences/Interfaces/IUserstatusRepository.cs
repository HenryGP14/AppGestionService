using Domain.Entities;

namespace Infraestructure.Presistences.Interfaces
{
    public interface IUserstatusRepository : IGenericRepository<Userstatus>
    {
        Task<Userstatus> GetUserstatusByIdAsync(int id);
        Task<IEnumerable<Userstatus>> GetListUserstatussAsync();
    }
}
