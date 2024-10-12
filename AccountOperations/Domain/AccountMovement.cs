using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountOperations.Domain
{
    public class AccountMovement
    {

        public DateTime Date { get; set; }
        public string CustomerIdentity { get; set; }
        public string CustomerName { get; set; }
        public string AccountNumber { get; set; }
        public AccountType Type { get; set; }
        public decimal InitialBalance { get; set; }
        public short State { get; set; }
        public decimal MovementAmount { get; set; }
        public decimal TotalBalance { get; set; }

    }
}
