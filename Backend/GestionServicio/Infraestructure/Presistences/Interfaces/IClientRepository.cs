using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;

namespace Infraestructure.Presistences.Interfaces
{
    public interface IClientRepository: IGenericRepository<Client>
    {
        Task<Client> GetClientByIdAsync(int id);
        Task<DataResponse<Client>> GetListClientsAsync(FiltersRequest request);
        Task<bool> GetClientByIdentificationAsync(string identification);
    }
}
