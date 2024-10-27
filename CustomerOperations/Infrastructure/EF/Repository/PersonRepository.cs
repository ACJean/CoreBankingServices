using CustomerOperations.Domain.Entity;
using CustomerOperations.Domain.Repository;
using CustomerOperations.Infrastructure.EF.Model;

namespace CustomerOperations.Infrastructure.EF.Repository
{
    public class PersonRepository : IPersonRepository
    {

        protected readonly CustomerDbContext _context;

        public PersonRepository(CustomerDbContext context) 
        {
            _context = context;
        }

        public int Add(Person entity)
        {
            DbPerson person = new()
            {
                Name = entity.Name,
                Gender = entity.Gender,
                Age = entity.Age,
                IdentityNumber = entity.IdentityNumber,
                Address = entity.Address,
                PhoneNumber = entity.PhoneNumber
            };

            _context.Persons.Add(person);
            _context.SaveChanges();

            return person.Id;
        }

        public void Delete(Person entity)
        {
            _context.Persons.Remove(new DbPerson { Id = entity.PersonId });
            _context.SaveChanges();
        }

        public IEnumerable<Person> GetAll()
        {
            return _context
                .Set<DbPerson>()
                .ToList()
                .Select(dbPerson =>
                {
                    return new Person
                    {
                        PersonId = dbPerson.Id,
                        Name = dbPerson.Name,
                        Gender = dbPerson.Gender,
                        Age = dbPerson.Age,
                        IdentityNumber = dbPerson.IdentityNumber,
                        Address = dbPerson.Address,
                        PhoneNumber = dbPerson.PhoneNumber
                    };
                });
        }

        public Person GetById(int id)
        {
            DbPerson dbPerson = _context.Set<DbPerson>().Find(id);

            Person person = dbPerson != null ? new()
            {
                PersonId = dbPerson.Id,
                Name = dbPerson.Name,
                Gender = dbPerson.Gender,
                Age = dbPerson.Age,
                IdentityNumber = dbPerson.IdentityNumber,
                Address = dbPerson.Address,
                PhoneNumber = dbPerson.PhoneNumber
            } : null;

            return person;
        }

        public void Update(Person entity)
        {
            DbPerson customer = new()
            {
                Id = entity.PersonId,
                Name = entity.Name,
                Gender = entity.Gender,
                Age = entity.Age,
                IdentityNumber = entity.IdentityNumber,
                Address = entity.Address,
                PhoneNumber = entity.PhoneNumber
            };

            _context.Set<DbPerson>().Update(customer);
            _context.SaveChanges();
        }
    }
}
