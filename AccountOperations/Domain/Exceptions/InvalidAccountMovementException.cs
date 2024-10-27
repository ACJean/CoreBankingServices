namespace AccountOperations.Domain.Exceptions
{
    public class InvalidAccountMovementException : Exception
    {
        public InvalidAccountMovementException() 
            : base("Saldo no disponible") { }

        public InvalidAccountMovementException(string message) 
            : base(message) { }

        public InvalidAccountMovementException(string message, Exception innerException) 
            : base(message, innerException) { }

    }
}
