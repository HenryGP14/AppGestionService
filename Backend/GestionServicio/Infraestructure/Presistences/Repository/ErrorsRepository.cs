using Domain.Entities;
using Infraestructure.Presistences.Contexts;
using Infraestructure.Presistences.Interfaces;

namespace Infraestructure.Presistences.Repository
{
    internal class ErrorsRepository : IErrorsRepository
    {
        private readonly GestionServicesContext _context;

        public ErrorsRepository(GestionServicesContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveErrorsByProcedure(Error request)
        {
            var result = await _context.InsertErrorRecordAsync(request);
            return result == 1 ? true : false;
        }
    }
}
