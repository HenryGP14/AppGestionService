using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;

namespace Infraestructure.Presistences.Interfaces
{
    public interface IUserRepository: IGenericRepository<User>
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUserNameAsync(string username);
        Task<DataResponse<User>> GetListUserAsync(FiltersRequest request, int userAuthId);
        Task<bool> ApproveUserAsync(int id, int userAuthId);
    }
}