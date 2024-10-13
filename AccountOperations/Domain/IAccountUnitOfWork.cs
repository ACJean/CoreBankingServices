using AccountOperations.Domain.Repository;
using SharedOperations.Domain;

namespace AccountOperations.Domain
{
    public interface IAccountUnitOfWork : IUnitOfWork
    {

        IAccountRepository Account { get; }
        IMovementsRepository Movements { get; }

    }
}
