using SharedOperations.Domain;

namespace CustomerOperations.Domain.Entity
{
    public class Customer : Person
    {

        public int CustomerId { get; set; }
        public string Password { get; set; }
        public short State { get; set; }

        public bool IsValidPassword(IPasswordValidator validator)
        {
            return validator.IsValid(Password);
        }

    }
}
