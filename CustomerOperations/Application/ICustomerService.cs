using CustomerOperations.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOperations.Application
{
    public interface ICustomerService
    {

        void Add(Customer customer);
        Customer? Get(string identityNumber);
        void Update(Customer customer);
        void Delete(string identityNumber);

    }
}
