using Application.Dtos.Request;
using Application.Dtos.Response;
using Domain.Entities;
using Infraestructure.Commons.Reponse;

namespace Application.Interfaces
{
    public interface IDeviceService
    {
        Task<GenericResponse<DataResponse<DeviceResponse>>> GetDeviceByServiceId(int serviceId);
        Task<GenericResponse<bool>> CreateDevice(DeviceRequest request, int serviceId, int userId);
        Task<GenericResponse<bool>> UpdateDevice(DeviceRequest request, int serviceId, int userId, int deviceId);
    }
}
