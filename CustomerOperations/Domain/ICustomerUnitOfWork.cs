using CustomerOperations.Infrastructure.EF.Model;
using SharedOperations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOperations.Domain
{
    public interface ICustomerUnitOfWork : IUnitOfWork
    {

        ICustomerRepository Customers { get; }
        IPersonRepository Persons { get; }

    }
}
