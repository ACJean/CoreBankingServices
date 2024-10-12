using CustomerOperations.Domain;
using CustomerOperations.Infrastructure.EF.Model;
using CustomerOperations.Infrastructure.EF.Repository;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOperations.Infrastructure.EF
{
    public class CustomerUnitOfWork : ICustomerUnitOfWork
    {

        private readonly CustomerDbContext _context;
        private IDbContextTransaction _transaction;

        public ICustomerRepository Customers { get; private set; }
        public IPersonRepository Persons { get; private set; }

        public CustomerUnitOfWork(CustomerDbContext context)
        {
            _context = context;
            Customers = new CustomerRepository(_context);
            Persons = new PersonRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose() => _context.Dispose();

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
            }
        }
    }
}
