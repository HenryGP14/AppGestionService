using Domain.Entities;
using Infraestructure.Presistences.Contexts;
using Infraestructure.Presistences.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Presistences.Repository
{
    internal class MethodpaymentRepository : GenericRepository<Methodpayment>, IMethodpaymentRepository
    {
        public MethodpaymentRepository(GestionServicesContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Methodpayment>> GetListMethodpaymentsAsync()
        {
            var listMethodpayments = await GetEntityQuery(det => det.Datedelete == null)
                .AsNoTracking()
                .ToListAsync();
            return listMethodpayments;
        }

        public Task<Methodpayment> GetMethodpaymentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
