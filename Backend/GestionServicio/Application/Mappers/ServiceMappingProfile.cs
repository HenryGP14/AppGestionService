using Application.Dtos.Request;
using Application.Dtos.Response;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Commons.Reponse;

namespace Application.Mappers
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<ServiceRequest, Service>()
                .ForMember(det => det.Servicedescription, opt => opt.MapFrom(src => $"{src.Description}. De {src.Velocity} Mbts"))
                .ForMember(det => det.Servicename, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
            CreateMap<Service, ServiceResponse>()
                .ReverseMap();
            CreateMap<DataResponse<ServiceResponse>, DataResponse<Service>>()
                .ReverseMap();
        }
    }
}
