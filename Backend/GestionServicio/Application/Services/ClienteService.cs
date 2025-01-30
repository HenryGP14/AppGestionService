using Application.Dtos.Request;
using Application.Dtos.Response;
using Application.Interfaces;
using Application.Validations;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;
using Infraestructure.Presistences.Interfaces;
using Microsoft.AspNetCore.Http;
using Utility.Static;

namespace Application.Services
{
    public class ClienteService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ClientValidator _validations;

        public ClienteService(IUnitOfWork unitOfWork, IMapper mapper, ClientValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validations = validationRules;
        }

        public async Task<GenericResponse<ClientResponse>> GetCLientById(int idClient)
        {
            var response = new GenericResponse<ClientResponse>();
            try
            {
                var client = await _unitOfWork.Client.GetClientByIdAsync(idClient);
                var mapClient = _mapper.Map<ClientResponse>(client);
                return SuccessResponse(response, mapClient);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "GetCLientById");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<GenericResponse<DataResponse<ClientResponse>>> GetListClients(FiltersRequest filtersRequest)
        {
            var response = new GenericResponse<DataResponse<ClientResponse>>();
            try
            {
                var clients = await _unitOfWork.Client.GetListClientsAsync(filtersRequest);
                if (clients == null)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND, StatusCodes.Status404NotFound);
                }
                return SuccessResponse(response, _mapper.Map<DataResponse<ClientResponse>>(clients), MessageHttpResponse.MESSAGE_SUCCESS);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "GetListClients");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<GenericResponse<bool>> CreateUserClient(ClientRequest clientRequest)
        {
            var response = new GenericResponse<bool>();
            try
            {
                var validationResult = await _validations.ValidateAsync(clientRequest);
                if (!validationResult.IsValid)
                {
                    response.Erros = validationResult.Errors;
                    return ErrorResponse(response, "Error por validación", StatusCodes.Status400BadRequest);
                }

                var clientRegister = _mapper.Map<Client>(clientRequest);
                clientRegister.Datecreation = DateTime.Now;
                var validateClientExists = await ValidateClientExists(clientRegister.Identification);
                if (validateClientExists)
                {
                    return ErrorResponse(response, $"El cliente con la CI {clientRegister.Identification} ya se encuentra registrado, verifica en el listado de clientes", StatusCodes.Status500InternalServerError);
                }
                var result = await _unitOfWork.Client.SaveAsync(clientRegister);
                if (!result)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_SAVE_REGISTER, StatusCodes.Status500InternalServerError);
                }
                return SuccessResponse(response, true);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "CreateUserClient");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<GenericResponse<bool>> UpdateUserClient(ClientRequest clientRequest, int idClient)
        {
            var response = new GenericResponse<bool>();
            try
            {
                var validationResult = await _validations.ValidateAsync(clientRequest);
                if (!validationResult.IsValid)
                {
                    response.Erros = validationResult.Errors;
                    return ErrorResponse(response, "Error por validación", StatusCodes.Status400BadRequest);
                }

                var clientUpdate = _mapper.Map<Client>(clientRequest);
                var clientExists = await _unitOfWork.Client.GetClientByIdAsync(idClient);
                var validateClientExists = await ValidateClientExists(clientUpdate.Identification);
                if (validateClientExists && !clientExists.Identification.Equals(clientRequest.Identification))
                {
                    return ErrorResponse(response, $"El cliente con la CI {clientUpdate.Identification} se encuentra registrado, verificar la CI", StatusCodes.Status500InternalServerError);
                }
                clientUpdate.Clientid = clientExists.Clientid;
                clientUpdate.Datecreation = clientExists.Datecreation;
                clientUpdate.Dateupdate = DateTime.Now;
                var result = await _unitOfWork.Client.UpdateAsync(clientUpdate);
                if (!result)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_SAVE_REGISTER, StatusCodes.Status500InternalServerError);
                }
                return SuccessResponse(response, true);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "UpdateUserClient");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        private async Task<bool> ValidateClientExists(string identification)
        {
            if (string.IsNullOrEmpty(identification))
            {
                return false;
            }
            var clientExists = await _unitOfWork.Client.GetClientByIdentificationAsync(identification);
            return clientExists;
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
