namespace AccountOperations.Domain.Entity
{
    public class Account
    {

        public string? Number { get; set; }
        public string CustomerIdentity { get; set; }
        public AccountType Type { get; set; }
        public decimal Balance { get; set; }
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
