using Application.Dtos.Request;
using Application.Dtos.Response;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;

namespace Application.Interfaces
{
    public interface IClientService
    {
        Task<GenericResponse<bool>> CreateUserClient(ClientRequest clientRequest);
        Task<GenericResponse<DataResponse<ClientResponse>>> GetListClients(FiltersRequest filtersRequest);
        Task<GenericResponse<ClientResponse>> GetCLientById(int idClient);
        Task<GenericResponse<bool>> UpdateUserClient(ClientRequest clientRequest, int idClient);
    }
}
