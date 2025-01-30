using Domain.Entities;

namespace Infraestructure.Presistences.Interfaces
{
    public interface IStatuscontractRepository : IGenericRepository<Statuscontract>
    {
        Task<Statuscontract> GetStatuscontractByIdAsync(int id);
        Task<IEnumerable<Statuscontract>> GetListStatuscontractsAsync();
    }
}
