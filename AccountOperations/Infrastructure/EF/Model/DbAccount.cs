using AccountOperations.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountOperations.Infrastructure.EF.Model
{

    [Table("Account")]
    public class DbAccount
    {

        [Key]
        [Column("Acc_Number")]
        [Required]
        public long Number { get; set; }

        [Column("Acc_CustomerIdentity")]
        [Required]
        public string CustomerIdentity {  get; set; }

        [Column("Acc_Type")]
        [Required]
        public AccountType Type { get; set; }

        [Column("Acc_Balance")]
        [Required]
        public decimal Balance { get; set; }

        [Column("Acc_State")]
        [Required]
        public short State { get; set; }

        public List<DbMovements> Movements { get; set; }

    }
}
