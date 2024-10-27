using AccountOperations.Domain;
using AccountOperations.Domain.Entity;
using Microsoft.Extensions.Logging;
using SharedOperations.Domain;
using SharedOperations.Domain.Services;
using System.Collections.Concurrent;
using System.Globalization;

namespace AccountOperations.Application
{
    public class DefaultReportService : IReportService
    {
        private readonly ILogger<DefaultReportService> _logger;
        private readonly IAccountUnitOfWork _unitOfWork;
        private readonly ICustomerResources _customerResources;

        public DefaultReportService(ILogger<DefaultReportService> logger, IAccountUnitOfWork unitOfWork, ICustomerResources customerResources)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _customerResources = customerResources;
        }

        public async Task<Result<IEnumerable<AccountMovement>, Error>> GetAccountMovements(string startDate, string endDate)
        {
			try
			{
				DateTime dtStartDate = DateTime.ParseExact(startDate, "dd-MM-yyyy", CultureInfo.CurrentCulture);
                DateTime dtEndDate = DateTime.ParseExact(endDate, "dd-MM-yyyy", CultureInfo.CurrentCulture);

                dtStartDate = dtStartDate.Date;
                dtEndDate = dtEndDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                List<AccountMovement> accountMovements = await _unitOfWork.Movements.GetAccountsMovementsAsync(dtStartDate, dtEndDate);

                if (accountMovements.Any())
                {
                    ConcurrentDictionary<string, string> customersAccount = new(
                        accountMovements
                        .Select(movement => movement.CustomerIdentity)
                        .Distinct()
                        .ToDictionary(identity => identity, identity => ""));

                    var tasks = customersAccount.Select(async customerAccount =>
                    {
                        string customerName = await _customerResources.GetName(customerAccount.Key);

                        if (string.IsNullOrEmpty(customerName)) return;

                        customersAccount.AddOrUpdate(customerAccount.Key, "", (_, _) => customerName);
                    });

                    await Task.WhenAll(tasks);

                    foreach (var accountMovement in accountMovements)
                    {
                        accountMovement.CustomerName = customersAccount[accountMovement.CustomerIdentity];
                    }
                }

                return accountMovements;
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex, "Format of date incorrect.");
                throw;
            }
			catch (Exception ex)
			{
                _logger.LogError(ex, "Error on generate report.");
                throw;
			}
        }

    }
}
