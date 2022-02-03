using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AudioSync.Core.DependencyInjection.Base
{
    public static class BaseCommonDependencies
    {
        public static IServiceCollection RegisterCommon(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .RegisterAppSettings(configuration)
                .RegisterAuditing()
                .RegisterCommonDependencies();

            return services;
        }
    }
}
