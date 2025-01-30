using Application.Dtos.Request;
using Application.Dtos.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Presistences.Interfaces;
using Microsoft.AspNetCore.Http;
using Utility.Static;

namespace Application.Services
{
    public class ContractService : IContractService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContractService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenericResponse<bool>> CreateContractClient(ContractRequest request)
        {
            var response = new GenericResponse<bool>();
            try
            {
                var contractRegister = _mapper.Map<Contract>(request);
                contractRegister.Datecreation = DateTime.Now;
                var result = await _unitOfWork.Contract.SaveAsync(contractRegister);
                if (!result)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND, StatusCodes.Status404NotFound);
                }
                return SuccessResponse(response, true);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "CreateContractClient");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<GenericResponse<ContractResponse>> GetContractByClientId(int clientId)
        {
            var response = new GenericResponse<ContractResponse>();
            try
            {
                var contract = await _unitOfWork.Contract.GetListContractByClientIdAsync(clientId);
                if (contract is null)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND, StatusCodes.Status404NotFound);
                }
                var mapContract = _mapper.Map<ContractResponse>(contract);
                return SuccessResponse(response, mapContract);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "GetContractByClientId");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<GenericResponse<bool>> UpdateContractCLient(ContractRequest request, int contractId)
        {
            var response = new GenericResponse<bool>();
            try
            {
                var contractOld = await _unitOfWork.Contract.GetContractByIdAsync(contractId);
                if (contractOld is null)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND, StatusCodes.Status404NotFound);
                }
                // Validar si existe el metodo de pago
                contractOld.MethodpaymentMethodpaymentid = request.MethodpaymentMethodpaymentid;
                var result = await _unitOfWork.Contract.UpdateAsync(contractOld);
                if (!result)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND, StatusCodes.Status404NotFound);
                }
                return SuccessResponse(response, true);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "UpdateContractCLient");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<GenericResponse<bool>> UpdateStateContract(UpgradeServiceRequest request)
        {
            var response = new GenericResponse<bool>();
            try
            {
                var contractOld = await _unitOfWork.Contract.GetContractByIdAsync(request.ContractId);
                if (contractOld is null)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND, StatusCodes.Status404NotFound);
                }

                var result = await _unitOfWork.Contract.UpdateStatuesContract(request.ContractId, request.StateContractId);
                if (!result)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND, StatusCodes.Status404NotFound);
                }
                return SuccessResponse(response, true);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "UpdateStateContract");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<GenericResponse<bool>> UpgradeServiceContract(UpgradeServiceRequest request)
        {
            var response = new GenericResponse<bool>();
            try
            {
                var contractOld = await _unitOfWork.Contract.GetContractByIdAsync(request.ContractId);
                if (contractOld is null)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND, StatusCodes.Status404NotFound);
                }

                var serviceNew = await _unitOfWork.Service.GetServiceByIdAsync(request.ServiceId);
                if (serviceNew is null)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND, StatusCodes.Status404NotFound);
                }

                var result = await _unitOfWork.Contract.UpgradeService(request.ContractId, request.ServiceId, request.StateContractId);
                if (!result)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND, StatusCodes.Status404NotFound);
                }
                return SuccessResponse(response, true);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "UpgradeServiceContract");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        private GenericResponse<T> ErrorResponse<T>(GenericResponse<T> response, string message, int statusCode)
        {
            response.Success = false;
            response.Message = message;
            response.StatusCode = statusCode;
            return response;
        }

        private GenericResponse<T> SuccessResponse<T>(GenericResponse<T> response, T data, string message = "")
        {
            response.Success = true;
            response.Message = string.IsNullOrEmpty(message) ? MessageHttpResponse.MESSAGE_SUCCESS : message;
            response.StatusCode = StatusCodes.Status200OK;
            response.Data = data;
            return response;
        }

        private async Task HandleExceptionAsync(Exception ex, string processName)
        {
            Error error = new()
            {
                Nameprocess = processName,
                Description = $"{ex.Message} - {ex.Source}",
                Ipcreation = "127.0.0.1",
                Usercreation = "system"
            };
            await _unitOfWork.Errors.SaveErrorsByProcedure(error);
        }
    }
}
