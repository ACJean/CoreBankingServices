using AccountOperations.Domain.Entity;
using SharedOperations.Domain;

namespace AccountOperations.Application
{
    public interface IAccountService
    {

        Task<Result<Unit, Error>> Add(Account account);
        Result<Account, Error> Get(long accountNumber);
        Task<Result<Unit, Error>> Update(long accountNumber, Account account);
        Result<Unit, Error> Delete(long accountNumber);

    }
}
