using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOperations.Infrastructure.EF.Model
{

    [Table("Person")]
    public class DbPerson
    {
        [Key]
        [Column("Per_Id")]
        public int Id { get; set; }

        [Column("Per_Name")]
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [Column("Per_Gender")]
        [MaxLength(10)]
        [Required]
        public string Gender { get; set; }

        [Column("Per_Age")]
        [Required]
        public short Age { get; set; }

        [Column("Per_IdentityNumber")]
        [MaxLength(length: 50)]
        [Required]
        public string IdentityNumber { get; set; }

        [Column("Per_Address")]
        [MaxLength(200)]
        public string Address { get; set; }

        [Column("Per_PhoneNumber")]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public DbCustomer Customer { get; set; }

    }
}
