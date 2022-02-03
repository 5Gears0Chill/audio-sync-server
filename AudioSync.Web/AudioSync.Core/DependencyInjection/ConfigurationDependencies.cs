using AudioSync.Core.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AudioSync.Core.DependencyInjection
{
    public static class ConfigurationDependencies
    {
        public static IServiceCollection RegisterAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DataConnectionOptions>(configuration.GetSection(DataConnectionOptions.DataConnections));
            return services;
        }
    }
}
