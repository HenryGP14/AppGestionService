using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;

namespace Infraestructure.Presistences.Interfaces
{
    public interface ICashRepository: IGenericRepository<Cash>
    {
        Task<Cash> GetCashByIdAsync(int id);
        Task<DataResponse<Cash>> GetListCashesAsync(FiltersRequest request);
    }
}
