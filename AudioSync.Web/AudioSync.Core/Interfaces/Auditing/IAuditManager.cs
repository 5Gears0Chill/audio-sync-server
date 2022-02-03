using System.Collections.Generic;
using AudioSync.Core.DataAccess.Models;

namespace AudioSync.Core.Interfaces.Auditing
{
    public interface IAuditManager
    {
        TEntity SetUpdateAudit<TEntity>(TEntity entity) where TEntity : BaseEntity;
        TEntity SetDeactivateAudit<TEntity>(TEntity entity) where TEntity : BaseEntity;
        TEntity SetReactivateAudit<TEntity>(TEntity entity) where TEntity : BaseEntity;
        TEntity SetNewAudit<TEntity>(TEntity entity) where TEntity : BaseEntity;
        IEnumerable<TEntity> SetAuditList<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity;
    }
}
