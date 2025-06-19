using Domain.Interfaces.Services;
using Domain.Interfaces.Servises;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services
{
    public static class Bindings
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IRoomTypeService, RoomTypeService>();
            return services;
        }
    }
}