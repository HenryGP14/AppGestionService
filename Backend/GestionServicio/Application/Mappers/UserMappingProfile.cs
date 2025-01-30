using Application.Dtos.Request;
using Application.Dtos.Response;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Presistences.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserRequest, User>()
                .ForMember(det => det.RolRolid, opt => opt.MapFrom(src => src.Rolid))
                .ForMember(det => det.UserstatusStatusid, opt => opt.MapFrom(src => src.Statusid))
                .ReverseMap();
            CreateMap<LoginRequest, User>();
            CreateMap<User, UserReponse>()
                .ForMember(det => det.Rol, opt => opt.MapFrom(src => src.RolRol.Rolname))
                .ForMember(det => det.Status, opt => opt.MapFrom(src => src.UserstatusStatus.Description))
                .ForMember(det => det.ApprovalDate, opt => opt.MapFrom(src => src.Dateapproval))
                .ForMember(det => det.Approval, opt => opt.MapFrom(src => src.Dateapproval != null))
                .ForMember(det => det.UserApproval, opt => opt.MapFrom<UserApprovalResolver>())
                .ReverseMap();
            CreateMap<DataResponse<User>, DataResponse<UserReponse>>()
                .ReverseMap();
        }
    }

    public class UserApprovalResolver : IValueResolver<User, UserReponse, string>
    {
        private readonly GestionServicesContext _context;

        public UserApprovalResolver(GestionServicesContext context)
        {
            _context = context;
        }

        public string Resolve(User source, UserReponse destination, string destMember, ResolutionContext context)
        {
            if (source.Userapproval == null)
                return null;

            var user = _context.Users.Find(source.Userapproval);
            return user?.Username;
        }
    }
}
