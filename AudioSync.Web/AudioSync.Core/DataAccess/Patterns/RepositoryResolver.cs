using Microsoft.Extensions.DependencyInjection;
using System;
using AudioSync.Core.Interfaces.DataAccess;

namespace AudioSync.Core.DataAccess.Patterns
{
    public class RepositoryResolver : IRepositoryResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public RepositoryResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TRepository Resolve<TRepository>()
        {
            try
            {
                return (TRepository)_serviceProvider.GetService(typeof(TRepository));
            }
            catch (Exception)
            {
                using IServiceScope serviceScope = _serviceProvider.CreateScope();
                IServiceProvider provider = serviceScope.ServiceProvider;
                return (TRepository)provider.GetService(typeof(TRepository));
            }
        }
    }
}
