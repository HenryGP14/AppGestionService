using Application.Dtos.Request;
using Application.Dtos.Response;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Commons.Reponse;

namespace Application.Mappers
{
    public class DeviceMappingProfile : Profile
    {
        public DeviceMappingProfile()
        {
            CreateMap<DeviceResponse, Device>()
                .ReverseMap();
            CreateMap<DeviceRequest, Device>()
                .ReverseMap();
            CreateMap<DataResponse<Device>, DataResponse<DeviceResponse>>()
                .ReverseMap();
        }
    }
}
