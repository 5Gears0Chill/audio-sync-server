using AudioSync.Core.DataAccess.Entities;
using AudioSync.Core.DataAccess.Models;
using AudioSync.Core.Interfaces.DataAccess;
using System.Threading.Tasks;

namespace AudioSync.Core.Interfaces.Repositories
{
    public interface IDeviceRepository : IRepositoryCore
    {
        /// <summary>
        /// Saves and updates device
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<DataResult<Device>> SaveDeviceAsync(Device entity);    
    }
}
