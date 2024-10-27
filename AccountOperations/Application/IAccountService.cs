using AccountOperations.Domain.Entity;
using SharedOperations.Domain;

namespace AccountOperations.Application
{
    public interface IAccountService
    {

        Task<Result<Unit, Error>> Add(Account account);
        Result<Account, Error> Get(string accountNumber);
        Task<Result<Unit, Error>> Update(string accountNumber, Account account);
        Result<Unit, Error> Delete(string accountNumber);

    }
}
