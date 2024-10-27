using AccountOperations.Domain;
using AccountOperations.Domain.Repository;
using AccountOperations.Infrastructure.EF.Repository;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountOperations.Infrastructure.EF
{
    public class AccountUnitOfWork : IAccountUnitOfWork
    {
        private readonly AccountDbContext _context;
        private IDbContextTransaction _transaction;

        public IAccountRepository Account { get; private set; }
        public IMovementsRepository Movements { get; private set; }

        public AccountUnitOfWork(AccountDbContext context)
        {
            _context = context;
            Account = new AccountRepository(_context);
            Movements = new MovementsRepository(_context);
        }

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

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose() => _context.Dispose();

        public void Rollback()
        {
            if (_transaction is not null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
            }
        }
    }
}
