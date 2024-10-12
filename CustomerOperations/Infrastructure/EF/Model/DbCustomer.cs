using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOperations.Infrastructure.EF.Model
{
    [Table("Customer")]
    public class DbCustomer
    {
        [Key]
        [Column("Cus_Id")]
        public int Id { get; set; }

        [Column("Cus_Password")]
        [MaxLength(100)]
        [Required]
        public string Password { get; set; }

        [Column("Cus_State")]
        [Required]
        public short State { get; set; }

        [ForeignKey("Person")]
        [Column("Cus_PersonId")]
        public int PersonId { get; set; }
        public DbPerson Person { get; set; }

    }
}
