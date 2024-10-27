namespace AccountOperations.Domain.Generator
{
    public class DefaultAccountNumberGenerator : IAccountNumberGenerator
    {
        public long GenerateAccountNumber()
        {
            return long.Parse(DateTime.Now.ToString("yyddMMHHmmss"));
        }
    }
}
