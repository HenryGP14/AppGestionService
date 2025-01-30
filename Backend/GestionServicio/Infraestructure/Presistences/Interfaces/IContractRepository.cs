using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;

namespace Infraestructure.Presistences.Interfaces
{
    public interface IContractRepository: IGenericRepository<Contract>
    {
        Task<Contract> GetContractByIdAsync(int id);
        Task<Contract> GetListContractByClientIdAsync(int ClientId);
        Task<bool> UpgradeService(int contractId, int serviceId, int stateId);
        Task<bool> UpdateStatuesContract(int contractId, int stateId);
    }
}
