using AccountOperations.Domain.Entity;
using SharedOperations.Domain.Repository;

namespace AccountOperations.Domain.Repository
{
    public interface IMovementsRepository : IRepository<Movements, int>
    {

        Task<List<AccountMovement>> GetAccountsMovementsAsync(DateTime startDate, DateTime endDate);

    }
}
