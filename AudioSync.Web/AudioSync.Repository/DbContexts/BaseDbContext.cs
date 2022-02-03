using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AudioSync.Core.DataAccess.Models;
using AudioSync.Core.Interfaces.DataAccess;

namespace AudioSync.Repository.DbContexts
{
    public class BaseDbContext : DbContext, IDbContext
    {
        public BaseDbContext()
        {
        }

        public BaseDbContext(DbContextOptions<AudioSyncDbContext> options)
           : base(options)
        {
        }

        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
            => base.Set<TEntity>();

        public async Task<int> DeleteAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            try
            {
                Remove(entity);
                return await SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(await GetFullErrorTextAndRollbackEntityChanges(ex), ex);
            }
        }

        public async Task<int> DeleteEnumerableAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            try
            {
                RemoveRange(entities);
                return await SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(await GetFullErrorTextAndRollbackEntityChanges(ex), ex);
            }
        }
        public async Task<TEntity> GetByIdAsync<TEntity>(object id) where TEntity : BaseEntity
        {
            return await Set<TEntity>().FindAsync(id);
        }

        public async Task<int> InsertAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            try
            {
                await AddAsync(entity);
                return await SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(await GetFullErrorTextAndRollbackEntityChanges(ex), ex);
            }
        }

        public async Task<int> InsertEnumerableAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            try
            {
                await AddRangeAsync(entities);
                return await SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(await GetFullErrorTextAndRollbackEntityChanges(ex), ex);
            }
        }

        public Task<int> SaveChangesAsync()
            => base.SaveChangesAsync();

        public void SetEntityState<TEntity>(TEntity entity, int state) where TEntity : BaseEntity
        {
            Attach(entity).State = (EntityState)state;
        }

        public async Task<int> UpdateAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            try
            {
                Update(entity);
                return await SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(await GetFullErrorTextAndRollbackEntityChanges(ex), ex);
            }
        }

        public async Task<int> ExecuteRawSql(string sql)
        {
            return await Database.ExecuteSqlRawAsync(sql);
        }

        public async Task<int> UpdateEnumerableAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            try
            {
                UpdateRange(entities);
                return await SaveChangesAsync();

            }
            catch (DbUpdateException ex)
            {
                throw new Exception(await GetFullErrorTextAndRollbackEntityChanges(ex), ex);
            }
        }

        protected async Task<string> GetFullErrorTextAndRollbackEntityChanges(DbUpdateException ex)
        {
            if (this is DbContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();
                entries.ForEach(entry =>
                {
                    try
                    {
                        entry.State = EntityState.Unchanged;
                    }
                    catch (InvalidOperationException)
                    {
                        //ignored
                    }
                });
            }

            try
            {
                await this.SaveChangesAsync();
                return ex.ToString();
            }
            catch (Exception e)
            {
                //if after the rollabck of changes the context is still not saving,
                //return the full text of the exception that occured when saving
                return e.ToString();
            }
        }
    }
}
