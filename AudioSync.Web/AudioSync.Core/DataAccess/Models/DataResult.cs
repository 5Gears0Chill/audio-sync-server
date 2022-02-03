namespace AudioSync.Core.DataAccess.Models
{
    public class DataResult<TEntity> : NonDataResult where TEntity: BaseEntity
    {
        public TEntity Data { get; private set; }

        public DataResult(int rowsAffected, TEntity data)
            :base(rowsAffected)
        {
            this.Data = data;
        }
    }
}
