using Microsoft.Extensions.DependencyInjection;
using AudioSync.Core.Interfaces.Repositories;
using AudioSync.Repository.Repositories;

namespace AudioSync.Repository.DependencyInjection.Base
{
    public static class RepositoryDependencies
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            return services;
        }
    }
}
