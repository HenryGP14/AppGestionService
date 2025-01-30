using Domain.Entities;

namespace Infraestructure.Presistences.Interfaces
{
    public interface IMethodpaymentRepository: IGenericRepository<Methodpayment>
    {
        Task<Methodpayment> GetMethodpaymentByIdAsync(int id);
        Task<IEnumerable<Methodpayment>> GetListMethodpaymentsAsync();
    }
}
