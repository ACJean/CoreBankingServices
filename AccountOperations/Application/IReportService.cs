using AccountOperations.Domain.Entity;
using SharedOperations.Domain;

namespace AccountOperations.Application
{
    public interface IReportService
    {

        Task<Result<IEnumerable<AccountMovement>, Error>> GetAccountMovements(string startDate, string endDate);

    }
}
