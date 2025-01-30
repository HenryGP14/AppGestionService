using Application.Dtos.Request;
using Application.Dtos.Response;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class ContractMappingProfile : Profile
    {
        public ContractMappingProfile()
        {
            CreateMap<Contract, ContractRequest>()
                .ReverseMap();

            CreateMap<Contract, ContractResponse>()
                .ForMember(des => des.NameClient, opt => opt.MapFrom(src => $"{src.ClientClient.Name} {src.ClientClient.Lastname}"))
                .ForMember(des => des.IdetificationClient, opt => opt.MapFrom(src => src.ClientClient.Identification))
                .ForMember(des => des.StatusContract, opt => opt.MapFrom(src => src.StatuscontractStatus.Description))
                .ForMember(des => des.MethPayment, opt => opt.MapFrom(src => src.MethodpaymentMethodpayment.Description))
                .ForMember(des => des.ServiceName, opt => opt.MapFrom(src => src.ServiceService.Servicename))
                .ForMember(des => des.ServiceDesciption, opt => opt.MapFrom(src => src.ServiceService.Servicedescription))
                .ReverseMap();
        }
    }
}
