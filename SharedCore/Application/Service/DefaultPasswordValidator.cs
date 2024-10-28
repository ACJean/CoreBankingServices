using SharedOperations.Domain;
using SharedOperations.Domain.Validator;
using System.Text.RegularExpressions;

namespace SharedOperations.Application.Service
{
    public class DefaultPasswordValidator : IPasswordValidator
    {

        public Result<Unit, Error> IsValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return new Error("PasswordValidator", "La contraseña no puede ser vacia.");

            var regex = new Regex(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$");

            bool isValid = regex.IsMatch(password);
            return !isValid ? new Error("PasswordValidator", "La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula, un número y un carácter especial.") : Unit.Value;
        }

    }
}
