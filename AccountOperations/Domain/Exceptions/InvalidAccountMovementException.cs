using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AccountOperations.Domain.Exceptions
{
    public class InvalidAccountMovementException : Exception
    {
        public InvalidAccountMovementException()
        {
        }

        public InvalidAccountMovementException(string? message) : base(message)
        {
        }

        public InvalidAccountMovementException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidAccountMovementException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
