using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Presistences.Contexts;
using Infraestructure.Presistences.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Presistences.Repository
{
    internal class DeviceRepository: GenericRepository<Device>, IDeviceRepository
    {
        private readonly GestionServicesContext _context;
        public DeviceRepository(GestionServicesContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DataResponse<Device>> GetListDevicesByServiceIdAsync(int serviceId)
        {
            var response = new DataResponse<Device>();
            var devices = GetEntityQuery(device => device.ServiceServiceid == serviceId).AsNoTracking();
            response.TotalRecords = await devices.CountAsync();
            response.Items = await devices.ToListAsync();
            return response;
        }

        public async Task<Device> GetDeviceById(int deviceId)
        {
            var device = await GetEntityQuery(device => device.Deviceid == deviceId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return device!;
        }
    }
}
