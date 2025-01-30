using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;

namespace Infraestructure.Presistences.Interfaces
{
    public interface IDeviceRepository: IGenericRepository<Device>
    {
        Task<DataResponse<Device>> GetListDevicesByServiceIdAsync(int serviceId);
        Task<Device> GetDeviceById(int deviceId);
    }
}
