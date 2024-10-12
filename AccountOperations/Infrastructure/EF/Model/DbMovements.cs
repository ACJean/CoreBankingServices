using AccountOperations.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountOperations.Infrastructure.EF.Model
{
    [Table("Movements")]
    public class DbMovements
    {

        [Key]
        [Column("Mov_Id")]
        public int Id { get; set; }

        [ForeignKey("Account")]
        [Column("Mov_AccountNumber")]
        public string AccountNumber { get; set; }
        public DbAccount Account { get; set; }

        [Column("Mov_Date")]
        public DateTime Date { get; set; }

        [Column("Mov_Type")]
        public AccountType Type { get; set; }

        [Column("Mov_Amount")]
        public decimal Amount { get; set; }

        [Column("Mov_Balance")]
        public decimal Balance { get; set; }

    }
}
