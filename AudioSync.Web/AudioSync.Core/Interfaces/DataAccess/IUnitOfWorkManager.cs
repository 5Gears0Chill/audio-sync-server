using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AudioSync.Core.DataAccess.Models;

namespace AudioSync.Core.Interfaces.DataAccess
{
    public interface IUnitOfWorkManager
    {
        Task<TResult> ExecuteAsyncComplete<TResult>(Func<IUnitOfWork, Task<TResult>> runQuery);
        Task<TResult> ExecuteMultiCallAsync<TResult>(Func<IUnitOfWork, Task<TResult>> runQuery);
        Task<TResult> ExecuteSingleSaveAsync<TRepository, TResult>(Func<TRepository, Task<TResult>> runQuery) where TResult : BaseEntity where TRepository : IRepositoryCore;
        Task<TResult> ExecuteSingleAsync<TRepository, TResult>(Func<TRepository, Task<TResult>> runQuery) where TRepository : IRepositoryCore;
        Task<IEnumerable<TResult>> ExecuteListAsync<TRepository, TResult>(Func<TRepository, Task<IEnumerable<TResult>>> runQuery) where TRepository : IRepositoryCore;
    }
}
