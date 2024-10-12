using SharedOperations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountOperations.Domain
{
    public interface IAccountUnitOfWork : IUnitOfWork
    {

        IAccountRepository Account { get; }
        IMovementsRepository Movements { get; }

    }
}
