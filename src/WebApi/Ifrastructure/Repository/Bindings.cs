using Domain.Interfaces.Repositories;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Repository
{
    public static class Bindings
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
            return services;
        }
    }
}