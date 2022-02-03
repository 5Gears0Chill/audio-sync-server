using AudioSync.Core.DataAccess.Entities;
using AudioSync.Core.DataAccess.Models;
using AudioSync.Core.DataAccess.Patterns;
using AudioSync.Core.Interfaces.Auditing;
using AudioSync.Core.Interfaces.Repositories;
using AudioSync.Repository.DbContexts;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AudioSync.Repository.Repositories
{
    public class DeviceRepository : Repository<AudioSyncDbContext>, IDeviceRepository
    {
        public DeviceRepository(IAuditManager auditManager, ILogger<Repository<AudioSyncDbContext>> logger) : base(auditManager, logger)
        {
        }

        /// <summary>
        /// Saves and updates device
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<DataResult<Device>> SaveDeviceAsync(Device entity)
        {
            return await SaveAsync(entity);
        }
    }
}
