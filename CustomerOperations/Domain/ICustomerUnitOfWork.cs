using CustomerOperations.Domain.Repository;
using SharedOperations.Domain;

namespace CustomerOperations.Domain
{
    public interface ICustomerUnitOfWork : IUnitOfWork
    {

        ICustomerRepository Customers { get; }
        IPersonRepository Persons { get; }

    }
}
