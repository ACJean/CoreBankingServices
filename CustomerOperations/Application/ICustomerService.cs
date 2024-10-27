using CustomerOperations.Domain.Entity;
using SharedOperations.Domain;

namespace CustomerOperations.Application
{
    public interface ICustomerService
    {

        Result<Unit, Error> Add(Customer customer);
        Result<Customer, Error> Get(string identityNumber);
        Result<Unit, Error> Update(Customer customer);
        Result<Unit, Error> Delete(string identityNumber);

    }
}
