namespace AudioSync.Core.Interfaces.DataAccess
{
    public interface IUnitOfWork
    {
        TRepository Get<TRepository>();
    }
}
