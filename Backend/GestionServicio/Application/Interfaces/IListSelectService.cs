using Application.Dtos.Response;

namespace Application.Interfaces
{
    public interface IListSelectService
    {
        Task<GenericResponse<IEnumerable<SelectListReponse>>> GetRol();
        Task<GenericResponse<IEnumerable<SelectListReponse>>> GetMethodpayment();
        Task<GenericResponse<IEnumerable<SelectListReponse>>> GetAttentionstatus();
        Task<GenericResponse<IEnumerable<SelectListReponse>>> GetAttentiontype();
        Task<GenericResponse<IEnumerable<SelectListReponse>>> GetUserstatus();
        Task<GenericResponse<IEnumerable<SelectListReponse>>> GetStatuscontract();
        Task<GenericResponse<IEnumerable<SelectListReponse>>> GetCashByUser(int userId);
    }
}
