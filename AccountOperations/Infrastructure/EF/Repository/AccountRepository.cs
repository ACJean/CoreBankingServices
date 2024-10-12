using AccountOperations.Domain;
using AccountOperations.Infrastructure.EF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AccountOperations.Infrastructure.EF.Repository
{
    public class AccountRepository : IAccountRepository
    {

        protected readonly AccountDbContext _context;

        public AccountRepository(AccountDbContext context)
        {
            _context = context;
        }

        public string Add(Account entity)
        {
            DbAccount account = new()
            {
                Number = entity.Number,
                CustomerIdentity = entity.CustomerIdentity,
                Type = entity.Type,
                State = 1
            };

            _context.Accounts.Add(account);
            _context.SaveChanges();

            return account.Number;
        }

        public void Delete(Account entity)
        {
            _context.Accounts.Update(new DbAccount { Number = entity.Number, State = 0 });
            _context.SaveChanges();
        }

        public IEnumerable<Account> GetAll()
        {
            return _context.Set<DbAccount>().ToList()
                .Select(dbAccount =>
                {
                    return new Account
                    {
                        Number = dbAccount.Number,
                        CustomerIdentity = dbAccount.CustomerIdentity,
                        Type = dbAccount.Type,
                        Balance = dbAccount.Balance,
                        State = dbAccount.State,
                        Movements = dbAccount.Movements.Select(m => new Movements
                        {
                            Id = m.Id,
                            Date = m.Date,
                            Type = m.Type,
                            Amount = m.Amount,
                            Balance = m.Balance
                        }).ToList()
                    };
                });
        }

        public Account? GetById(string id)
        {
            DbAccount? dbAccount = _context.Accounts
                .Include(c => c.Movements)
                .AsNoTracking()
                .FirstOrDefault(c => c.Number == id);

            Account? entity = dbAccount != null ? new()
            {
                Number = dbAccount.Number,
                CustomerIdentity = dbAccount.CustomerIdentity,
                Type = dbAccount.Type,
                Balance = dbAccount.Balance,
                State = dbAccount.State,
                Movements = dbAccount.Movements.Select(m => new Movements
                {
                    Id = m.Id,
                    Date = m.Date,
                    Type = m.Type,
                    Amount = m.Amount,
                    Balance = m.Balance
                }).ToList()
            } : null;

            return entity;
        }

        public void Update(Account entity)
        {
            DbAccount account = new()
            {
                Number = entity.Number,
                CustomerIdentity = entity.CustomerIdentity,
                Type = entity.Type,
                Balance = entity.Balance
            };

            _context.Accounts.Update(account);
            _context.SaveChanges();
        }
    }
}
