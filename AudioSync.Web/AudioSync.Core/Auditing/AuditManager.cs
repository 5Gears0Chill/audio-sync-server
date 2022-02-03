using System;
using System.Collections.Generic;
using AudioSync.Core.DataAccess.Models;
using AudioSync.Core.Interfaces.Auditing;

namespace AudioSync.Core.Auditing
{
    public class AuditManager : IAuditManager
    {
        public TEntity SetUpdateAudit<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            if (entity is AuditBaseEntity)
            {
                var auditEntity = entity as AuditBaseEntity;
                auditEntity.LastUpdatedOn = DateTime.Now;

            }
            return entity;
        }

        public TEntity SetDeactivateAudit<TEntity>(TEntity entity) where TEntity: BaseEntity
        {
            entity.IsActive = false;
            if (entity is AuditBaseEntity)
            {
                var auditEntity = entity as AuditBaseEntity;
                auditEntity.LastUpdatedOn = DateTime.Now;
            }
            return entity;
        }

        public TEntity SetReactivateAudit<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            entity.IsActive = true;
            if (entity is AuditBaseEntity)
            {
                var auditEntity = entity as AuditBaseEntity;
                auditEntity.LastUpdatedOn = DateTime.Now;
            }
            return entity;
        }

        public TEntity SetNewAudit<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            entity.IsActive = true;
            if (entity is AuditBaseEntity)
            {
                var auditEntity = entity as AuditBaseEntity;
                auditEntity.CreatedOn = DateTime.Now;
                auditEntity.LastUpdatedOn = DateTime.Now;
            }
            return entity;
        }

        public IEnumerable<TEntity> SetAuditList<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            var list = new List<TEntity>();
            foreach (TEntity entity in entities)
            {
                if (entity.Id > 0)
                {
                    list.Add(SetUpdateAudit(entity));
                }
                else
                {
                    list.Add(SetNewAudit(entity));
                }
            }
            return list;
        }
    }
}
