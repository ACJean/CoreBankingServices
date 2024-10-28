using CustomerOperations.Domain.Entity;
using CustomerOperations.Domain.Repository;
using CustomerOperations.Infrastructure.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace CustomerOperations.Infrastructure.EF.Repository
{
    public class CustomerRepository : ICustomerRepository
    {

        protected readonly CustomerDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public CustomerRepository(CustomerDbContext context, IServiceProvider serviceProvider) 
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public int Add(Customer entity)
        {
            DbCustomer customer = new()
            {
                PersonId = entity.PersonId,
                Password = entity.Password,
                State = entity.State
            };
            
            _context.Set<DbCustomer>().Add(customer);
            _context.SaveChanges();

            return customer.Id;
        }

        public void Delete(Customer entity)
        {
            _context.Customers.Remove(new DbCustomer { Id = entity.CustomerId });
            _context.SaveChanges();
        }

        public void Update(Customer entity)
        {
            DbCustomer customer = new()
            {
                Id = entity.CustomerId,
                PersonId = entity.PersonId,
                Password = entity.Password,
                State = entity.State
            };

            _context.Customers.Update(customer);
            _context.SaveChanges();
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context
                .Customers
                .AsNoTracking()
                .ToList()
                .Select(dbCustomer =>
                {
                    Customer customer = new ()
                    { 
                        CustomerId = dbCustomer.Id,
                        PersonId = dbCustomer.PersonId,
                        Name = dbCustomer.Person.Name,
                        Gender = dbCustomer.Person.Gender,
                        Age = dbCustomer.Person.Age,
                        IdentityNumber = dbCustomer.Person.IdentityNumber,
                        Address = dbCustomer.Person.Address,
                        State = dbCustomer.State
                    };
                    customer.SetPhoneNumber(dbCustomer.Person.PhoneNumber);
                    customer.SetPassword(dbCustomer.Password);

                    return customer;
                });
        }

        public Customer GetById(int id)
        {
            DbCustomer customer = _context.Customers
                .Include(c => c.Person)
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id);
            
            Customer entity = customer != null ? new()
            {
                CustomerId = customer.Id,
                PersonId = customer.PersonId,
                Name = customer.Person.Name,
                Gender = customer.Person.Gender,
                Age = customer.Person.Age,
                IdentityNumber = customer.Person.IdentityNumber,
                Address = customer.Person.Address,
                State = customer.State
            } : null;

            if (entity is not null)
            {
                entity.SetPhoneNumber(customer.Person.PhoneNumber);
                entity.SetPassword(customer.Password);
            }

            return entity;
        }

        public Customer GetByIdentityNumber(string identityNumber)
        {
            DbCustomer customer = _context.Customers
                .Include(c => c.Person)
                .AsNoTracking()
                .FirstOrDefault(c => c.Person.IdentityNumber == identityNumber);

            Customer entity = customer != null ? new()
            {
                CustomerId = customer.Id,
                PersonId = customer.PersonId,
                Name = customer.Person.Name,
                Gender = customer.Person.Gender,
                Age = customer.Person.Age,
                IdentityNumber = customer.Person.IdentityNumber,
                Address = customer.Person.Address,
                State = customer.State
            } : null;

            if (entity is not null)
            {
                entity.SetPhoneNumber(customer.Person.PhoneNumber);
                entity.SetPassword(customer.Password);
            }

            return entity;
        }
    }
}
