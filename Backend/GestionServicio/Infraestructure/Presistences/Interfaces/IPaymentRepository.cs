using Domain.Entities;
using Infraestructure.Commons.Reponse;

namespace Infraestructure.Presistences.Interfaces
{
    public interface IPaymentRepository: IGenericRepository<Payment>
    {
        Task<DataResponse<Payment>> GetPaymentByIdClientAsync(int id);
    }
}
