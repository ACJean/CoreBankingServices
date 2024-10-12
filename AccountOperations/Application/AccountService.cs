using AccountOperations.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AccountOperations.Application
{
    public class AccountService
    {

        private readonly ILogger<AccountService> _logger;
        private readonly IAccountUnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountService(ILogger<AccountService> logger, IAccountUnitOfWork unitOfWork, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
        }


        public async Task Add(Account account)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("CustomerService");

                var response = await client.GetAsync($"/clientes/{account.CustomerIdentity}");
                
                if (!response.IsSuccessStatusCode || (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.NoContent))
                {
                    throw new InvalidOperationException("Customer not found.");
                }

                account.Number = Guid.NewGuid()
                    .ToString()
                    .Replace("-", "")
                    .Substring(0, 10);

                _unitOfWork.Account.Add(account);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogError(ex, "Error on create customer.");
                throw;
            }
        }

        public Account? Get(string accountNumber)
        {
            try
            {
                return _unitOfWork.Account.GetById(accountNumber);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogError(ex, "Error on get customer.");
                throw;
            }
        }

        public void Update(Account account)
        {
            try
            {
                _unitOfWork.Account.Update(account);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogError(ex, "Error on update customer.");
                throw;
            }
        }

        public void Delete(string accountNumber)
        {
            try
            {
                _unitOfWork.Account.Delete(new Account { Number = accountNumber });
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogError(ex, "Error on delete customer.");
                throw;
            }
        }
    }
}
