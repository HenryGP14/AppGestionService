using Application.Dtos.Request;
using Application.Dtos.Response;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Commons.Reponse;

namespace Application.Mappers
{
    internal class CashMappingProfile : Profile
    {
        public CashMappingProfile() {
            CreateMap<Cash, CashRequest>()
                .ForMember(det => det.Description, opt => opt.MapFrom(src => src.Cashdescription))
                .ForMember(det => det.Status, opt => opt.MapFrom(src => src.Active))
                .ReverseMap();
            CreateMap<Cash, CashResponse>()
                 .ForMember(det => det.Description, opt => opt.MapFrom(src => src.Cashdescription))
                 .ForMember(det => det.Status, opt => opt.MapFrom(src => src.Active.Equals("Y") ? "Activo" : "Inactivo"))
                .ReverseMap();
            CreateMap<DataResponse<CashResponse>, DataResponse<Cash>>()
                .ReverseMap();
        }
    }
}
