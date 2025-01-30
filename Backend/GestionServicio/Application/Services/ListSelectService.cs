using Application.Dtos.Response;
using Application.Interfaces;
using AutoMapper;
using Infraestructure.Presistences.Interfaces;
using Microsoft.AspNetCore.Http;
using Utility.Static;

namespace Application.Services
{
    internal class ListSelectService : IListSelectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ListSelectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GenericResponse<IEnumerable<SelectListReponse>>> GetAttentionstatus()
        {
            var response = new GenericResponse<IEnumerable<SelectListReponse>>();
            var list = await _unitOfWork.Attentionstatus.GetListAttentionsStatusAsync();
            return SuccessResponse(response, _mapper.Map<IEnumerable<SelectListReponse>>(list));
        }

        public async Task<GenericResponse<IEnumerable<SelectListReponse>>> GetAttentiontype()
        {
            var response = new GenericResponse<IEnumerable<SelectListReponse>>();
            var list = await _unitOfWork.Attentiontype.GetListAttentiontypesAsync();
            return SuccessResponse(response, _mapper.Map<IEnumerable<SelectListReponse>>(list));
        }

        public async Task<GenericResponse<IEnumerable<SelectListReponse>>> GetCashByUser(int userId)
        {
            var response = new GenericResponse<IEnumerable<SelectListReponse>>();
            var list = await _unitOfWork.Usercash.GetListCashByUser(userId);
            return SuccessResponse(response, _mapper.Map<IEnumerable<SelectListReponse>>(list));
        }

        public async Task<GenericResponse<IEnumerable<SelectListReponse>>> GetMethodpayment()
        {
            var response = new GenericResponse<IEnumerable<SelectListReponse>>();
            var list = await _unitOfWork.Methodpayment.GetListMethodpaymentsAsync();
            return SuccessResponse(response, _mapper.Map<IEnumerable<SelectListReponse>>(list));
        }

        public async Task<GenericResponse<IEnumerable<SelectListReponse>>> GetRol()
        {
            var response = new GenericResponse<IEnumerable<SelectListReponse>>();
            var list = await _unitOfWork.Rol.GetListRolesAsync();
            return SuccessResponse(response, _mapper.Map<IEnumerable<SelectListReponse>>(list));
        }

        public async Task<GenericResponse<IEnumerable<SelectListReponse>>> GetStatuscontract()
        {
            var response = new GenericResponse<IEnumerable<SelectListReponse>>();
            var list = await _unitOfWork.Statuscontract.GetListStatuscontractsAsync();
            return SuccessResponse(response, _mapper.Map<IEnumerable<SelectListReponse>>(list));
        }

        public async Task<GenericResponse<IEnumerable<SelectListReponse>>> GetUserstatus()
        {
            var response = new GenericResponse<IEnumerable<SelectListReponse>>();
            var list = await _unitOfWork.Userstatus.GetListUserstatussAsync();
            return SuccessResponse(response, _mapper.Map<IEnumerable<SelectListReponse>>(list));
        }

        private GenericResponse<T> SuccessResponse<T>(GenericResponse<T> response, T data, string message = "")
        {
            response.Success = true;
            response.Message = string.IsNullOrEmpty(message) ? MessageHttpResponse.MESSAGE_SUCCESS : message;
            response.StatusCode = StatusCodes.Status200OK;
            response.Data = data;
            return response;
        }
    }
}
