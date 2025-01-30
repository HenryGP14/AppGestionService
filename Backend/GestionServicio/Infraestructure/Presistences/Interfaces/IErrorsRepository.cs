using Domain.Entities;

namespace Infraestructure.Presistences.Interfaces
{
    public interface IErrorsRepository
    {
        Task<bool> SaveErrorsByProcedure(Error request);
    }
}
