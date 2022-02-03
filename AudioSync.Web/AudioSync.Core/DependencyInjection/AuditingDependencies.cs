using AudioSync.Core.Interfaces.Auditing;
using Microsoft.Extensions.DependencyInjection;
using AudioSync.Core.Auditing;

namespace AudioSync.Core.DependencyInjection
{
    public static class AuditingDependencies
    {
        public static IServiceCollection RegisterAuditing(this IServiceCollection services)
        {
            services.AddScoped<IAuditManager, AuditManager>();

            return services;
        }
    }
}
