using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SharedOperations.Domain.Exceptions
{
    public class CustomerAlreadyExistException : Exception
    {
        public CustomerAlreadyExistException()
        {
        }

        public CustomerAlreadyExistException(string? message) : base(message)
        {
        }

        public CustomerAlreadyExistException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CustomerAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
