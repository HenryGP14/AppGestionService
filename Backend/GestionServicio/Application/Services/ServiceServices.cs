using Application.Dtos.Request;
using Application.Dtos.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;
using Infraestructure.Presistences.Interfaces;
using Microsoft.AspNetCore.Http;
using Utility.Static;

namespace Application.Services
{
    public class ServiceServices : IServiceServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenericResponse<DataResponse<ServiceResponse>>> GetListServices(FiltersRequest request)
        {
            var response = new GenericResponse<DataResponse<ServiceResponse>>();
            try
            {
                var services = await _unitOfWork.Service.GetListServcesAsync(request);
                if (services is null)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND, StatusCodes.Status404NotFound);
                }
                var mapServices = _mapper.Map<DataResponse<ServiceResponse>>(services);
                return SuccessResponse(response, mapServices);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "GetListServices");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<GenericResponse<ServiceResponse>> GetServiceById(int idService)
        {
            var response = new GenericResponse<ServiceResponse>();
            try
            {
                var service = await _unitOfWork.Service.GetServiceByIdAsync(idService);
                if (service is null)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND, StatusCodes.Status404NotFound);
                }
                var mapService = _mapper.Map<ServiceResponse>(service);
                return SuccessResponse(response, mapService);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "GetServiceById");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<GenericResponse<bool>> CreateService(ServiceRequest request, int userId)
        {
            var response = new GenericResponse<bool>();
            try
            {
                var userAuth = await ValidateUserAsync(userId);
                if (userAuth is null)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND_USER, StatusCodes.Status404NotFound);
                }
                if (userAuth.RolRolid != (int)UserRole.Administrador)
                {
                    return ErrorResponse(response, "Tu usario no es apto para crear un servicio", StatusCodes.Status401Unauthorized);
                }
                var service = _mapper.Map<Service>(request);
                service.Datecreation = DateTime.Now;
                var result = await _unitOfWork.Service.SaveAsync(service);
                if (!result)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_SAVE_REGISTER, StatusCodes.Status404NotFound);
                }
                return SuccessResponse(response, true);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "CreateService");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<GenericResponse<bool>> UpdateService(ServiceRequest request, int idService, int userId)
        {
            var response = new GenericResponse<bool>();
            try
            {
                var userAuth = await ValidateUserAsync(userId);
                if (userAuth is null)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND_USER, StatusCodes.Status404NotFound);
                }
                if (userAuth.RolRolid != (int)UserRole.Administrador)
                {
                    return ErrorResponse(response, "Tu usario no tiene permitido editar un servicio", StatusCodes.Status401Unauthorized);
                }
                var serviceExists = await _unitOfWork.Service.GetServiceByIdAsync(idService);

                if (serviceExists is null)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND, StatusCodes.Status404NotFound);
                }
                var service = _mapper.Map<Service>(request);
                service.Serviceid = serviceExists.Serviceid;
                service.Datecreation = serviceExists.Datecreation;
                service.Dateupdate = DateTime.Now;

                var result = await _unitOfWork.Service.UpdateAsync(service);
                if (!result)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND, StatusCodes.Status404NotFound);
                }
                return SuccessResponse(response, true);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "UpdateService");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        private async Task<User?> ValidateUserAsync(int userId)
        {
            if (userId == 0)
                return null;

            var userExists = await _unitOfWork.User.GetUserByIdAsync(userId);
            if (userExists == null)
                return null;

            return userExists;
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
