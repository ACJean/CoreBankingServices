using SharedOperations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOperations.Domain.Errors
{
    internal class CustomerErrors
    {

        public static readonly Error NotFound = new("Customer.NotFound", 
            "Customer not found.");

        public static readonly Error AlreadyExist = new("Customer.AlreadyExist",
            "A customer already exists.");

    }
}
