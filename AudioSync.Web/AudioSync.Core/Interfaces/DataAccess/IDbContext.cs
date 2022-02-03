using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AudioSync.Core.DataAccess.Models;

namespace AudioSync.Core.Interfaces.DataAccess
{
    /// <summary>
    /// Represent DbContext
    /// </summary>
    public partial interface IDbContext : IDisposable
    {

        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        Task<int> SaveChangesAsync();

        void SetEntityState<TEntity>(TEntity entity, int state) where TEntity : BaseEntity;
        /// <summary>
        /// Gets an entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        Task<TEntity> GetByIdAsync<TEntity>(object id) where TEntity : BaseEntity;

        /// <summary>
        /// Inserts an entity async
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Success Result</returns>
        Task<int> InsertAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;

        /// <summary>
        /// Inserts multiple entities async
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <returns>Success Result and Rows Affected</returns>
        Task<int> InsertEnumerableAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity;

        /// <summary>
        /// Update an entity async
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Success Result and Rows Affected</returns>
        Task<int> UpdateAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;

        /// <summary>
        /// Update multiple entities async
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <returns>Success Result and Rows Affected</returns>
        Task<int> UpdateEnumerableAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity;

        /// <summary>
        /// Delete an entity async
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Success Result and Rows Affected</returns>
        Task<int> DeleteAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;

        /// <summary>
        /// Delete multiple entities async
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <returns>Success Result and Rows Affected</returns>
        Task<int> DeleteEnumerableAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity;

        /// <summary>
        /// execute raw sql on db
        /// </summary>
        /// <param name="sql">sql to be run</param>
        /// <returns>number of rows affected</returns>
        Task<int> ExecuteRawSql(string sql);

    }
}
