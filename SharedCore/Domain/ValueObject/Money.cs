
using System.Globalization;

namespace SharedOperations.Domain.ValueObject
{
    public class Money : ValueObject
    {

        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public Money Add(Money other)
        {
            CheckCurrency(other);
            return new Money(Amount + other.Amount, Currency);
        }

        public Money Subtract(Money other)
        {
            CheckCurrency(other);
            return new Money(Amount - other.Amount, Currency);
        }

        public Money Multiply(Money other)
        {
            return new Money(Amount * other.Amount, Currency);
        }

        public Money Divide(Money other)
        {
            if (other.Amount == 0)
                throw new DivideByZeroException("El divisor no puede ser cero.");

            return new Money(Amount / other.Amount, Currency);
        }

        private void CheckCurrency(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("No se pueden operar cantidades en diferentes monedas.");
        }

        public override bool Equals(object obj) => Equals(obj as Money);

        public bool Equals(Money other) => other != null && Amount == other.Amount && Currency == other.Currency;

        public override int GetHashCode() =>
            HashCode.Combine(Amount, Currency);

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return Currency;
        }

        public override string ToString()
        {
            return Amount.ToString(NumberFormatInfo.InvariantInfo) + " - " + Currency;
        }

        public static bool operator ==(Money left, Money right) => EqualityComparer<Money>.Default.Equals(left, right);
        public static bool operator !=(Money left, Money right) => !(left == right);
        public static Money operator +(Money left, Money right) => left.Add(right);
        public static Money operator -(Money left, Money right) => left.Subtract(right);
        public static Money operator *(Money left, Money right) => left.Multiply(right);
        public static Money operator /(Money left, Money right) => left.Divide(right);

    }
}
