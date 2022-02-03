using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AudioSync.Core.DataAccess.Models;
using AudioSync.Core.Interfaces.Auditing;
using AudioSync.Core.Interfaces.DataAccess;

namespace AudioSync.Core.DataAccess.Patterns
{
    public partial class Repository<TContext> : IRepositoryCore
        where TContext : IDbContext
    {
        public IAuditManager _auditManager { get; set; }
        public ILogger<Repository<TContext>> _logger;
        public Repository(IAuditManager auditManager, ILogger<Repository<TContext>> logger)
        {
            _auditManager = auditManager;
            _logger = logger;
        }

        protected TContext Context;

        public void SetContext(IDbContext context)
        {
            Context = (TContext)context;
        }

        public void DisposeContext()
        {
            Context.Dispose();
        }

        public async Task<DataResult<TEntity>> SaveAsync<TEntity>(TEntity entity, bool skipAudit = false) where TEntity : BaseEntity
        {
            try
            {
                int rowsAffected;
                if (entity.Id > 0)
                {
                    if(!skipAudit)
                        entity = _auditManager.SetUpdateAudit(entity);
                    rowsAffected = await Context.UpdateAsync(entity);
                }
                else
                {
                    entity = _auditManager.SetNewAudit(entity);
                    rowsAffected = await Context.InsertAsync(entity);
                }
                return new DataResult<TEntity>(rowsAffected, entity);
            }catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new DataResult<TEntity>(0,null);
            }
        }

        public async Task<DataListResult<TEntity>> InsertListAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            entities = _auditManager.SetAuditList(entities);
            var rowsAffected = await Context.InsertEnumerableAsync(entities);
            return new DataListResult<TEntity>(rowsAffected, entities);
        }

        public async Task<DataListResult<TEntity>> SaveListAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            entities = _auditManager.SetAuditList(entities);
            var newEntities = new List<TEntity>();
            var numberOfAffectedRows = 0;
            foreach(var e in entities)
            {
                try
                {
                    var result = await SaveAsync(e);
                    if (result.IsSuccessful)
                    {
                        newEntities.Add(result.Data);
                        numberOfAffectedRows += result.RowsAffected;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    return new DataListResult<TEntity>(0, entities);
                }
            }
            return new DataListResult<TEntity>(numberOfAffectedRows, newEntities);
        }

        public async Task<DataListResult<TEntity>> UpdateListAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            entities = _auditManager.SetAuditList(entities);
            var rowsAffected = await Context.UpdateEnumerableAsync(entities);
            return new DataListResult<TEntity>(rowsAffected, entities);
        }

        public async Task<DataResult<TEntity>> DeleteAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            var rowsAffected = await Context.DeleteAsync(entity);
            return new DataResult<TEntity>(rowsAffected, entity);
        }

        public async Task<DataResult<TEntity>> SoftDeleteAsync<TEntity>(TEntity entity) where TEntity: BaseEntity
        {
            entity = _auditManager.SetDeactivateAudit(entity);
            var rowsAffected = await Context.UpdateAsync(entity);
            return new DataResult<TEntity>(rowsAffected, entity);
        }

        public async Task<DataResult<TEntity>> UnSoftDeleteAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            entity = _auditManager.SetReactivateAudit(entity);
            var rowsAffected = await Context.UpdateAsync(entity);
            return new DataResult<TEntity>(rowsAffected, entity);
        }

        public async Task<DataListResult<TEntity>> DeleteListAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            var rowsAffected = await Context.DeleteEnumerableAsync(entities);
            return new DataListResult<TEntity>(rowsAffected, entities);
        }

        public async Task<int> ExecuteRawSqlAsync(string sql)
        {
            return await Context.ExecuteRawSql(sql);
        }
    }
}
