using SharedOperations.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOperations.Domain
{
    public class Customer : Person
    {

        public int CustomerId { get; set; }
        public string Password { get; set; }
        public short State { get; set; }

        public bool IsValidPassword(IPasswordValidator validator)
        {
            return validator.IsValid(Password);
        }

    }
}
