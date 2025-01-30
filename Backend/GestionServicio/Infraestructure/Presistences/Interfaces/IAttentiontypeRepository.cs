using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;

namespace Infraestructure.Presistences.Interfaces
{
    public interface IAttentiontypeRepository: IGenericRepository<Attentiontype>
    {
        Task<Attentiontype> GetAttentiontypeByIdAsync(int id);
        Task<IEnumerable<Attentiontype>> GetListAttentiontypesAsync();
    }
}
