using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOperations.Domain
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public short Age { get; set; }
        public string IdentityNumber { get; set; }
        public string Address { get; set; }

        [RegularExpression(@"^\d{7}(\d{2})?$", ErrorMessage = "El número de telefono no es valido")]
        public string PhoneNumber { get; set; }

    }
}
