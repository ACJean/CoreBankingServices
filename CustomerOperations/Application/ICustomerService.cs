using CustomerOperations.Domain.Entity;
using SharedOperations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOperations.Application
{
    public interface ICustomerService
    {

        Result<Unit, Error> Add(Customer customer);
        Result<Customer?, Error> Get(string identityNumber);
        Result<Unit, Error> Update(Customer customer);
        Result<Unit, Error> Delete(string identityNumber);

    }
}
