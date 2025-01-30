using Azure.Core;
using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;
using Infraestructure.Presistences.Contexts;
using Infraestructure.Presistences.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Presistences.Repository
{
    internal class PaymentRepository: GenericRepository<Payment>, IPaymentRepository
    {
        private readonly GestionServicesContext _context;
        public PaymentRepository(GestionServicesContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DataResponse<Payment>> GetPaymentByIdClientAsync(int id)
        {
            var response = new DataResponse<Payment>();
            var request = new PaginationRequest();
            request.Sort = "Paymentdate";
            var payments = GetEntityQuery(payment => payment.ClientClientid == id && payment.Dateupdate == null)
                                                  .AsNoTracking();
            response.TotalRecords = payments.Count();
            response.Items = await Ordering(request, payments, true).ToListAsync(); ;
            return response!;
        }
    }
}
