using System.Collections.Generic;
using System.Linq;

namespace AudioSync.Core.DataAccess.Models
{
    public class DataListResult<TEntity> where TEntity : BaseEntity
    {
        public int TotalRecords { get; }
        public IEnumerable<TEntity> Data { get; private set; }

        public int RowsAffected { get; private set; }

        public virtual bool IsSuccessful
        {
            get
            {
                return RowsAffected > 0;
            }
        }
        public DataListResult(int rowsAffected, IEnumerable<TEntity> data)
        {
            TotalRecords = data.Count();
            this.Data = data;
            this.RowsAffected = rowsAffected;
        }
    }
}
