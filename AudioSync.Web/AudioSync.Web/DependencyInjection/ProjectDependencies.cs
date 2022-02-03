using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AudioSync.Repository.DependencyInjection.Base;

namespace AudioSync.API.DependencyInjection
{
    public static class ProjectDependencies
    {
        public static IServiceCollection RegisterAllDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterRepository(configuration);
            return services;
        }
    }
}
