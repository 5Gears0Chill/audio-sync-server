namespace AudioSync.Core.Interfaces.DataAccess
{
    public interface IRepositoryCore
    {
        void SetContext(IDbContext context);
        void DisposeContext();
    }
}