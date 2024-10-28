using SharedOperations.Domain;
using SharedOperations.Domain.Validator;

namespace CustomerOperations.Domain.Entity
{
    public class Person
    {

        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public short Age { get; set; }
        public string IdentityNumber { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; private set; }

        public Result<Unit, Error> SetPhoneNumber(string phoneNumber, IPhoneNumberValidator phoneNumberValidator)
        {
            var result = phoneNumberValidator.IsValid(phoneNumber);
            return result.Match(
                success =>
                {
                    PhoneNumber = phoneNumber;
                    return result;
                },
                error =>
                {
                    return result;
                }); ;
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

    }
}
