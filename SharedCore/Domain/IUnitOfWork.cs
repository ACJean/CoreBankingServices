namespace SharedOperations.Domain
{
    public interface IUnitOfWork : IDisposable
    {

        void BeginTransaction();
        void Commit();
        void Rollback();
        int Complete();

    }
}
