using Application.Dtos.Request;
using Application.Dtos.Response;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;

namespace Application.Interfaces
{
    public interface IServiceServices
    {
        Task<GenericResponse<DataResponse<ServiceResponse>>> GetListServices(FiltersRequest request);
        Task<GenericResponse<ServiceResponse>> GetServiceById(int idService);
        Task<GenericResponse<bool>> CreateService(ServiceRequest request, int userId);
        Task<GenericResponse<bool>> UpdateService(ServiceRequest request, int idService, int userId);
    }
}
