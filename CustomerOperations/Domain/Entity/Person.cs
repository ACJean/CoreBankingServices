using System.ComponentModel.DataAnnotations;

namespace CustomerOperations.Domain.Entity
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
