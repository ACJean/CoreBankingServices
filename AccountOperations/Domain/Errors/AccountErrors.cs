using SharedOperations.Domain;

namespace AccountOperations.Domain.Errors
{
    public class AccountErrors
    {

        public static readonly Error NotFound = new("Account.NotFound",
            "Account not found.");

    }
}
