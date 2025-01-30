using Application.Dtos.Request;
using Application.Dtos.Response;

namespace Application.Interfaces
{
    public interface IContractService
    {
        Task<GenericResponse<ContractResponse>> GetContractByClientId(int clientId);
        Task<GenericResponse<bool>> CreateContractClient(ContractRequest request);
        Task<GenericResponse<bool>> UpdateContractCLient(ContractRequest request, int contractId);
        Task<GenericResponse<bool>> UpgradeServiceContract(UpgradeServiceRequest request);
        Task<GenericResponse<bool>> UpdateStateContract(UpgradeServiceRequest request);
    }
}
