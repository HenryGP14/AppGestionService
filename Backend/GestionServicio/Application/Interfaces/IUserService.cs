using Application.Dtos.Request;
using Application.Dtos.Response;
using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<GenericResponse<DataResponse<UserReponse>>> GetListUser(FiltersRequest request, int userAuthId);
        Task<GenericResponse<UserReponse>> GetUserById(int id);
        Task<GenericResponse<string>> GetLoginToken(LoginRequest request);
        Task<GenericResponse<bool>> CreateUser(UserRequest user);
        Task<GenericResponse<bool>> ApproveUser(int userId, int userAuthId);
    }
}
