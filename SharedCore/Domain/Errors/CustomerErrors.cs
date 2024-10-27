using SharedOperations.Domain;

namespace SharedOperations.Errors
{
    public class CustomerErrors
    {

        public static readonly Error NotFound = new("Customer.NotFound", 
            "Customer not found.");

        public static readonly Error AlreadyExist = new("Customer.AlreadyExist",
            "A customer already exists.");

    }
}
