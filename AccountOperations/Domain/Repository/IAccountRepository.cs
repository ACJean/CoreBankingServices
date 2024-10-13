using AccountOperations.Domain.Entity;
using SharedOperations.Domain.Repository;

namespace AccountOperations.Domain.Repository
{
    public interface IAccountRepository : IRepository<Account, string>
    {

    }
}
