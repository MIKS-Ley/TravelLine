using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Bindings
    {
        public static IServiceCollection AddInfrastructureBindings(this IServiceCollection services)
        {
            services.AddRepositories();
            services.AddServices();
            return services;
        }
    }
}