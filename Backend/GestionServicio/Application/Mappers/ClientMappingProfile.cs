using Application.Dtos.Request;
using Application.Dtos.Response;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Commons.Reponse;

namespace Application.Mappers
{
    public class ClientMappingProfile : Profile
    {
        public ClientMappingProfile()
        {
            CreateMap<Client, ClientRequest>().ReverseMap();
            CreateMap<Client, ClientResponse>().ReverseMap();
            CreateMap<DataResponse<Client>, DataResponse<ClientResponse>>().ReverseMap();
        }
    }
}
