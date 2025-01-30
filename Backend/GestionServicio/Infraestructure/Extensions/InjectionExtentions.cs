using Infraestructure.Presistences.Contexts;
using Infraestructure.Presistences.Interfaces;
using Infraestructure.Presistences.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Extensions
{
    public static class InjectionExtentions
    {
        public static IServiceCollection ServicesInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(GestionServicesContext).Assembly.FullName;
            services.AddDbContext<GestionServicesContext>(optionsAction => optionsAction.UseSqlServer(
                    configuration.GetConnectionString("ConnectionSQL"), b => b.MigrationsAssembly(assembly)
                ), ServiceLifetime.Transient);
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
