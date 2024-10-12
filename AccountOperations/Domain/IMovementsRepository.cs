using SharedOperations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountOperations.Domain
{
    public interface IMovementsRepository : IRepository<Movements, int>
    {

        Task<List<AccountMovement>> GetAccountsMovementsAsync(DateTime startDate, DateTime endDate);

    }
}
