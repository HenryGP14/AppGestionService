using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;

namespace Infraestructure.Presistences.Interfaces
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        Task<Service> GetServiceByIdAsync(int id);
        Task<DataResponse<Service>> GetListServcesAsync(FiltersRequest request);
    }
}
