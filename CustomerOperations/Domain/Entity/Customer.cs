using SharedOperations.Domain;
using SharedOperations.Domain.Validator;

namespace CustomerOperations.Domain.Entity
{
    public class Customer : Person
    {

        public int CustomerId { get; set; }
        public string Password { get; private set; }
        public short State { get; set; }

        public Result<Unit, Error> SetPassword(string password, IPasswordValidator passwordValidator)
        {
            var result = passwordValidator.IsValid(password);
            return result.Match(
                success =>
                {
                    Password = password;
                    return result;
                },
                error =>
                {
                    return result;
                });
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public void Disable() => State = 0;
        public void Enable() => State = 1;

    }
}
