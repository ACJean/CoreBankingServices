using AccountOperations.Domain;
using AccountOperations.Infrastructure.EF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountOperations.Infrastructure.EF.Repository
{
    public class MovementsRepository : IMovementsRepository
    {

        protected readonly AccountDbContext _context;

        public MovementsRepository(AccountDbContext context)
        {
            _context = context;
        }

        public int Add(Movements entity)
        {
            DbMovements dbMovements = new()
            {
                AccountNumber = entity.AccountNumber,
                Date = entity.Date,
                Type = entity.Type,
                Amount = entity.Amount,
                Balance = entity.Balance
            };

            _context.Movements.Add(dbMovements);
            _context.SaveChanges();

            return dbMovements.Id;
        }

        public void Delete(Movements entity)
        {
            _context.Movements.Remove(new DbMovements { Id = entity.Id });
            _context.SaveChanges();
        }

        public IEnumerable<Movements> GetAll()
        {
            return _context.Set<DbMovements>().ToList()
                .Select(dbMovement =>
                {
                    return new Movements
                    {
                        Id = dbMovement.Id,
                        Date = dbMovement.Date,
                        Type = dbMovement.Type,
                        Amount = dbMovement.Amount,
                        Balance = dbMovement.Balance,
                        AccountNumber = dbMovement.AccountNumber,
                        Account = dbMovement.Account != null ? new Account
                        {
                            Number = dbMovement.Account.Number,
                            Type = dbMovement.Account.Type,
                            Balance = dbMovement.Account.Balance,
                            State = dbMovement.Account.State
                        } : null
                    };
                });
        }

        public Movements? GetById(int id)
        {
            DbMovements? dbMovement = _context.Movements
                .Include(c => c.Account)
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id);

            Movements? entity = dbMovement != null ? new()
            {
                Id = dbMovement.Id,
                Date = dbMovement.Date,
                Type = dbMovement.Type,
                Amount = dbMovement.Amount,
                Balance = dbMovement.Balance,
                AccountNumber = dbMovement.AccountNumber,
                Account = dbMovement.Account != null ? new Account
                {
                    Number = dbMovement.Account.Number,
                    Type = dbMovement.Account.Type,
                    Balance = dbMovement.Account.Balance,
                    State = dbMovement.Account.State
                } : null
            } : null;

            return entity;
        }

        public void Update(Movements entity)
        {
            DbMovements dbMovement = new()
            {
                Id = entity.Id,
                AccountNumber = entity.AccountNumber,
                Date = entity.Date,
                Type = entity.Type,
                Amount = entity.Amount,
                Balance = entity.Balance
            };

            _context.Set<DbMovements>().Update(dbMovement);
            _context.SaveChanges();
        }

        public async Task<List<AccountMovement>> GetAccountsMovementsAsync(DateTime startDate, DateTime endDate)
        {
            List<DbMovements> accounts = await _context.Movements
                .Include(m => m.Account)
                .AsNoTracking()
                .Where(m => m.Date >= startDate && m.Date <= endDate)
                .ToListAsync();

            return accounts.Select(movement => new AccountMovement
            {
                Date = movement.Date,
                CustomerIdentity = movement.Account.CustomerIdentity,
                AccountNumber = movement.Account.Number,
                Type = movement.Type,
                InitialBalance = movement.Balance,
                State = movement.Account.State,
                MovementAmount = movement.Amount,
                TotalBalance = movement.Balance + movement.Amount
            }).ToList();
        }

    }
}
