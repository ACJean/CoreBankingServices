using CustomerOperations.Domain.Entity;
using SharedOperations.Domain.Repository;

namespace CustomerOperations.Domain.Repository
{
    public interface ICustomerRepository : IRepository<Customer, int>
    {

        Customer? GetByIdentityNumber(string identityNumber);

    }
}
