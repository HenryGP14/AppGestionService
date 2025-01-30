using Application.Interfaces;
using Application.Mappers;
using Application.Services;
using Infraestructure.Presistences.Contexts;
using Infraestructure.Presistences.Interfaces;
using Infraestructure.Presistences.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICashService, CashService>();
            services.AddScoped<IClientService, ClienteService>();
            services.AddScoped<IServiceServices, ServiceServices>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<ITurnService, TurnService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IListSelectService, ListSelectService>();
            return services;
        }
    }
}
