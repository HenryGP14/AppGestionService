using Application.Dtos.Request;
using Application.Dtos.Response;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;

namespace Application.Interfaces
{
    public interface ICashService
    {
        Task<GenericResponse<bool>> CreateCashAsync(CashRequest cashRequest);
        Task<GenericResponse<bool>> UpdateCashAsync(CashRequest cashRequest, int cashId);
        Task<GenericResponse<CashResponse>> GetCashByIdAsync(int cashId, int userId);
        Task<GenericResponse<DataResponse<CashResponse>>> GetListCashesAsync(FiltersRequest request);
        Task<GenericResponse<bool>> AsignateUserCash(CashAsignateRequest cashAsignateRequest);
    }
}
