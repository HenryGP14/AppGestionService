using Application.Dtos.Request;
using Application.Dtos.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Presistences.Interfaces;
using Microsoft.AspNetCore.Http;
using Utility.Static;

namespace Application.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeviceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenericResponse<DataResponse<DeviceResponse>>> GetDeviceByServiceId(int serviceId)
        {
            var response = new GenericResponse<DataResponse<DeviceResponse>>();
            try
            {
                var serviceExists = await _unitOfWork.Service.GetServiceByIdAsync(serviceId);
                if (serviceExists is null)
                {
                    return ErrorResponse(response, "No se encontró el servicio a consultar", StatusCodes.Status404NotFound);
                }
                var deviceService = await _unitOfWork.Device.GetListDevicesByServiceIdAsync(serviceId);
                if (deviceService is null)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND, StatusCodes.Status404NotFound);
                }
                var mapDevice = _mapper.Map<DataResponse<DeviceResponse>>(deviceService);
                return SuccessResponse(response, mapDevice);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "GetDeviceByServiceId");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<GenericResponse<bool>> CreateDevice(DeviceRequest request, int serviceId, int userId)
        {
            var response = new GenericResponse<bool>();
            try
            {
                var serviceExists = await _unitOfWork.Service.GetServiceByIdAsync(serviceId);
                if (serviceExists is null)
                {
                    return ErrorResponse(response, "No se encontró el servicio a consultar", StatusCodes.Status404NotFound);
                }
                var userAuth = await ValidateUserAsync(userId);
                if (userAuth is null)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND_USER, StatusCodes.Status404NotFound);
                }
                if (userAuth.RolRolid != (int)UserRole.Administrador)
                {
                    return ErrorResponse(response, "Tu usuario no está permitido crear un producto al servicio", StatusCodes.Status401Unauthorized);
                }
                var deviceService = _mapper.Map<Device>(request);
                deviceService.ServiceServiceid = serviceId;
                deviceService.Datecreation = DateTime.Now;

                var result = await _unitOfWork.Device.SaveAsync(deviceService);
                if (!result)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_SAVE_REGISTER, StatusCodes.Status404NotFound);
                }
                return SuccessResponse(response, true);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "CreateDevice");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<GenericResponse<bool>> UpdateDevice(DeviceRequest request, int serviceId, int userId, int deviceId)
        {
            var response = new GenericResponse<bool>();
            try
            {
                var serviceExists = await _unitOfWork.Service.GetServiceByIdAsync(serviceId);
                if (serviceExists is null)
                {
                    return ErrorResponse(response, "No se encontró el servicio a consultar", StatusCodes.Status404NotFound);
                }
                var userAuth = await ValidateUserAsync(userId);
                if (userAuth is null)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND_USER, StatusCodes.Status404NotFound);
                }
                if (userAuth.RolRolid != (int)UserRole.Administrador)
                {
                    return ErrorResponse(response, "Tu usuario no está permitido crear un producto al servicio", StatusCodes.Status401Unauthorized);
                }

                var deviceOld = await _unitOfWork.Device.GetDeviceById(deviceId);
                if (deviceOld is null)
                {
                    return ErrorResponse(response, "No se encontró el producto dentro del servicio", StatusCodes.Status404NotFound);
                }

                var deviceService = _mapper.Map<Device>(request);
                deviceService.Deviceid = deviceOld.Deviceid;
                deviceService.ServiceServiceid = serviceId;
                deviceService.Datecreation = deviceOld.Datecreation;
                deviceService.Dateupdate = DateTime.Now;

                var result = await _unitOfWork.Device.UpdateAsync(deviceService);
                if (!result)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_SAVE_REGISTER, StatusCodes.Status404NotFound);
                }
                return SuccessResponse(response, true);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "UpdateDevice");
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
