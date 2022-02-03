using Microsoft.Extensions.DependencyInjection;
using AudioSync.Core.DataAccess.Patterns;
using AudioSync.Repository.DbContexts;
using AudioSync.Core.Interfaces.DataAccess;

namespace AudioSync.Repository.DependencyInjection.Base
{
    public static class DataAccessDependencies
    {
        public static IServiceCollection RegisterRepositoryPatterns(this IServiceCollection services)
        {
            return services
                .RegisterUnitOfWorkManagers();
        }

        private static IServiceCollection RegisterUnitOfWorkManagers(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryResolver, RepositoryResolver>();
            services.AddScoped<IUnitOfWork, UnitOfWork<AudioSyncDbContext>>();
            services.AddScoped<IUnitOfWorkManager, UnitOfWorkManager<AudioSyncDbContext>>();

            return services;
        }
    }
}
