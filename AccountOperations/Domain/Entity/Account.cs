using AccountOperations.Domain.Exceptions;

namespace AccountOperations.Domain.Entity
{
    public class Account
    {

        private decimal _balance;

        public string Number { get; set; }
        public string CustomerIdentity { get; set; }
        public AccountType Type { get; set; }
        public decimal Balance { 
            get 
            { 
                return _balance; 
            }
            set 
            { 
                if (value < 0) throw new InvalidAccountMovementException();
                _balance = value;
            }
        }
        public short State { get; set; }

        public List<Movements> Movements { get; set; }

        public Account()
        {
            Movements = new List<Movements>();
        }

    }

    public enum AccountType
    {
        Checking = 1,
        Savings = 2
    }
}
