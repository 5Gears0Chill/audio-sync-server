using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AudioSync.Core.DependencyInjection.Base;

namespace AudioSync.Repository.DependencyInjection.Base
{
    public static class BaseRepositoryDependencies
    {
        public static IServiceCollection RegisterRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .RegisterCommon(configuration)
                .RegisterRepositoryPatterns()
                .RegisterRepositories();

            return services;
        }
    }
}
