using SharedOperations.Domain;
using SharedOperations.Domain.Validator;
using System.Text.RegularExpressions;

namespace SharedOperations.Application.Service
{
    public class DefaultPhoneNumberValidator : IPhoneNumberValidator
    {
        public Result<Unit, Error> IsValid(string phoneNumber)
        {
            var regex = new Regex(@"^\d{7}(\d{2})?$");
            bool isValid = regex.IsMatch(phoneNumber);
            return !isValid ? new Error("PhoneNumberValidator", "El número de telefono no es valido") : Unit.Value;
        }
    }
}
