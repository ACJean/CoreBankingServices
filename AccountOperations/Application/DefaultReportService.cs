using AccountOperations.Domain;
using AccountOperations.Domain.Entity;
using Microsoft.Extensions.Logging;
using SharedOperations.Domain.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<AccountMovement>> GetAccountMovements(string startDate, string endDate)
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
                        string? customerName = await _customerResources.GetName(customerAccount.Key);

                        if (string.IsNullOrEmpty(customerName)) return;

                        customersAccount.TryAdd(customerAccount.Key, customerName);
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
                _logger.LogError(ex, "Error on create customer.");
                throw;
			}
        }

    }
}
