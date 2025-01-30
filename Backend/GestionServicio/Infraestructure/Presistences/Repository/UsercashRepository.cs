using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Presistences.Contexts;
using Infraestructure.Presistences.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Presistences.Repository
{
    internal class UsercashRepository : GenericRepository<Usercash>, IUsercashRepository
    {
        private readonly GestionServicesContext _context;
        public UsercashRepository(GestionServicesContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usercash>> GetListCashByUser(int idUser)
        {
            var userCash = await GetEntityQuery(usercash => usercash.UserUserid == idUser)
                .Include(cash => cash.CashCash)
                .AsNoTracking()
                .ToListAsync();

            return userCash;
        }

        public async Task<DataResponse<Usercash>> GetListUsercashsByUserAndByCashIdAsync(int userId, int cashId)
        {
            var response = new DataResponse<Usercash>();
            var userCash = GetEntityQuery(usercash => usercash.UserUserid == userId && usercash.CashCashid == cashId)
                .AsNoTracking();

            response.Items = await userCash.ToListAsync();
            response.TotalRecords = response.Items.Count;
            return response;
        }

        public async Task<DataResponse<Usercash>> GetUsercashByCashIdAsync(int cashId)
        {
            var response = new DataResponse<Usercash>();
            var userCash = GetEntityQuery(usercash => usercash.CashCashid == cashId)
                .AsNoTracking();

            response.Items = await userCash.ToListAsync();
            response.TotalRecords = response.Items.Count;
            return response;
        }
    }
}
