using Application.Dtos.Response;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class SelectListMappingProfile : Profile
    {
        public SelectListMappingProfile()
        {
            CreateMap<Userstatus, SelectListReponse>()
                .ForMember(det => det.Id, opt => opt.MapFrom(det => det.Statusid))
                .ForMember(det => det.Value, opt => opt.MapFrom(det => det.Description))
                .ReverseMap();

            CreateMap<Rol, SelectListReponse>()
                .ForMember(det => det.Id, opt => opt.MapFrom(det => det.Rolid))
                .ForMember(det => det.Value, opt => opt.MapFrom(det => det.Rolname))
                .ReverseMap();

            CreateMap<Methodpayment, SelectListReponse>()
                .ForMember(det => det.Id, opt => opt.MapFrom(det => det.Methodpaymentid))
                .ForMember(det => det.Value, opt => opt.MapFrom(det => det.Description))
                .ReverseMap();

            CreateMap<Statuscontract, SelectListReponse>()
                .ForMember(det => det.Id, opt => opt.MapFrom(det => det.Statusid))
                .ForMember(det => det.Value, opt => opt.MapFrom(det => det.Description))
                .ReverseMap();

            CreateMap<Attentionstatus, SelectListReponse>()
                .ForMember(det => det.Id, opt => opt.MapFrom(det => det.Statusid))
                .ForMember(det => det.Value, opt => opt.MapFrom(det => det.Description))
                .ReverseMap();

            CreateMap<Attentiontype, SelectListReponse>()
                .ForMember(det => det.Id, opt => opt.MapFrom(det => det.Attentiontypeid))
                .ForMember(det => det.Value, opt => opt.MapFrom(det => det.Description))
                .ReverseMap();

            CreateMap<Usercash, SelectListReponse>()
                .ForMember(det => det.Id, opt => opt.MapFrom(det => det.CashCashid))
                .ForMember(det => det.Value, opt => opt.MapFrom(det => det.CashCash.Cashdescription))
                .ReverseMap();
        }
    }
}
