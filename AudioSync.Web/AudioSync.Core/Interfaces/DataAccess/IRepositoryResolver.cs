namespace AudioSync.Core.Interfaces.DataAccess
{
    public interface IRepositoryResolver
    {
        TRepository Resolve<TRepository>();
    }
}
