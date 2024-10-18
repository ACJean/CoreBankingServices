using AccountOperations.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountOperations.Application
{
    public interface IAccountService
    {

        Task Add(Account account);
        Account? Get(string accountNumber);
        Task Update(string accountNumber, Account account);
        void Delete(string accountNumber);

    }
}
