using System.ComponentModel.DataAnnotations;

namespace AccountOperations.Domain.Entity
{
    public class Movements
    {

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public AccountType Type { get; set; }

        [CustomValidation(typeof(Movements), nameof(ValidateNotZero))]
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }

        public string AccountNumber { get; set; }
        public Account? Account { get; set; }

        public static ValidationResult ValidateNotZero(int value, ValidationContext context)
        {
            if (value == 0)
            {
                return new ValidationResult("Movement amount cannot be 0.");
            }
            return ValidationResult.Success;
        }
    }
}
