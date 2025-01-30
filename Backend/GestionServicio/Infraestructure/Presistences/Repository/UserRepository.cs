using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;
using Infraestructure.Presistences.Contexts;
using Infraestructure.Presistences.Interfaces;
using Microsoft.EntityFrameworkCore;
using Utility.Static;

namespace Infraestructure.Presistences.Repository
{
    internal class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly GestionServicesContext _context;
        public UserRepository(GestionServicesContext context) : base(context)
        {
            _context = context;
        }

        // Obtener todo el listado de usuarios
        public async Task<DataResponse<User>> GetListUserAsync(FiltersRequest request, int userAuthId)
        {
            var response = new DataResponse<User>();
            var users = GetEntityQuery(user => user.Datedelete == null)
                .Include(user => user.RolRol)
                .Include(user => user.UserstatusStatus)
                .AsNoTracking();

            request.Sort = string.IsNullOrEmpty(request.Sort) ? "Userid" : request.Sort;

            if (request.NumFilter is not null)
            {
                switch (request.NumFilter)
                {
                    case 1:
                        users = users.Where(user => user.Username.Contains(request.TextFilter!));
                        break;
                    case 2:
                        users = users.Where(user => user.Email.Contains(request.TextFilter!));
                        break;
                    case 3:
                        users = users.Where(user => user.RolRolid.Equals((int)UserRole.Cajero) && user.Usercreate.Equals(userAuthId));
                        break;
                }
            }

            if(request.StateFilter is not null)
            {
                users = users.Where(user => user.UserstatusStatusid.Equals(request.StateFilter));
            }

            if(!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                var startDate = Convert.ToDateTime(request.StartDate);
                var endDate = Convert.ToDateTime(request.EndDate);
                users = users.Where(user => user.Datecreation >= startDate && user.Datecreation <= endDate);
            }

            response.TotalRecords = users.Count();
            response.Items = await Ordering(request, users, true).ToListAsync();
            return response;
        }
        // Obtener un usuario por su id
        public async Task<User> GetUserByIdAsync(int id)
        {
            var response = await _context.Users
                .Include(user => user.RolRol)
                .Include(user => user.UserstatusStatus)
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Userid.Equals(id));
            return response!;
        }
        // Obtener un usuario por su nombre de usuario
        public async Task<User> GetUserByUserNameAsync(string username)
        {
            var response = await _context.Users
                .AsNoTracking()
                .Include(user => user.RolRol)
                .Include(user => user.UserstatusStatus)
                .FirstOrDefaultAsync(user => user.Username.Equals(username)); 
            return response!;
        }

        public async Task<bool> ApproveUserAsync(int id, int userAuthId)
        {
            var userApprove = await GetUserByIdAsync(id);
            if(userApprove is null)
                return false;

            userApprove.Userapproval = userAuthId;
            userApprove.Dateapproval = DateTime.Now;
            var response = await UpdateAsync(userApprove);
            if (!response)
                return false;

            return true;
        }
    }
}
