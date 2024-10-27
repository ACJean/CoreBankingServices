namespace SharedOperations.Domain.Exceptions
{
    public class CustomerAlreadyExistException : Exception
    {
        public CustomerAlreadyExistException() 
            : base("Customer already exist.") { }

        public CustomerAlreadyExistException(string message) 
            : base(message) { }

        public CustomerAlreadyExistException(string message, Exception innerException) 
            : base(message, innerException) { }

    }
}
