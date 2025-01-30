using Domain.Entities;
using Infraestructure.Commons.Reponse;

namespace Infraestructure.Presistences.Interfaces
{
    public interface IUsercashRepository : IGenericRepository<Usercash>
    {
        Task<DataResponse<Usercash>> GetUsercashByCashIdAsync(int cashId);
        Task<DataResponse<Usercash>> GetListUsercashsByUserAndByCashIdAsync(int userId, int cashId);
        Task<IEnumerable<Usercash>> GetListCashByUser(int idUser);
    }
}
