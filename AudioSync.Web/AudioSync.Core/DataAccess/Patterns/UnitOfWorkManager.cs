using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AudioSync.Core.Interfaces.DataAccess;
using AudioSync.Core.DataAccess.Models;

namespace AudioSync.Core.DataAccess.Patterns
{
    public class UnitOfWorkManager<TContext> : IUnitOfWorkManager where TContext : DbContext, IDbContext
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbContextFactory<TContext> _contextFactory;

        public UnitOfWorkManager(IUnitOfWork unitOfWork, IDbContextFactory<TContext> contextFactory)
        {
            _unitOfWork = unitOfWork;
            _contextFactory = contextFactory;
        }

        public TContext CreateContextInstance()
        {
            return _contextFactory.CreateDbContext();
        }
       
        public async Task<TResult> ExecuteAsyncComplete<TResult>(Func<IUnitOfWork, Task<TResult>> runQuery)
        {
            TResult result;
            result = await runQuery(_unitOfWork);

            return result;
        }

        public async Task<TResult> ExecuteMultiCallAsync<TResult>(Func<IUnitOfWork, Task<TResult>> runQuery)
        {
            TResult result;
            result = await runQuery(_unitOfWork);

            return result;
        }

        public async Task<TResult> ExecuteSingleSaveAsync<TRepository, TResult>(Func<TRepository, Task<TResult>> runQuery)
            where TResult : BaseEntity
            where TRepository : IRepositoryCore
        {
            TResult result;
            var repository = _unitOfWork.Get<TRepository>();
            var baseRepository = repository as Repository<TContext>;
            repository.SetContext(CreateContextInstance());
            result = await runQuery(repository);
            await baseRepository.SaveAsync(result);
            repository.DisposeContext();

            return result;
        }

        public async Task<TResult> ExecuteReadAsync<TResult>(Func<IUnitOfWork, Task<TResult>> runQuery)
        {
            TResult result;
            result = await runQuery(_unitOfWork);

            return result;
        }

        public async Task<TResult> ExecuteSingleAsync<TRepository, TResult>(Func<TRepository, Task<TResult>> runQuery)
             where TRepository : IRepositoryCore
        {
            TResult result;
            var repository = _unitOfWork.Get<TRepository>();
            result = await WrappedContext(repository, runQuery);

            return result;
        }

        public async Task<IEnumerable<TResult>> ExecuteListAsync<TRepository, TResult>(Func<TRepository, Task<IEnumerable<TResult>>> runQuery)
            where TRepository : IRepositoryCore
        {
            IEnumerable<TResult> result;
            var repository = _unitOfWork.Get<TRepository>();
            result = await WrappedContext(repository, runQuery);

            return result;
        }

        private async Task<TResult> WrappedContext<TRepository, TResult>(TRepository repository, Func<TRepository, Task<TResult>> runQuery)
              where TRepository : IRepositoryCore
        {
            TResult result = default;
            using (var context = CreateContextInstance())
            {
                repository.SetContext(CreateContextInstance());
                result = await runQuery(repository);
            }

            return result;
        }
    }
}
