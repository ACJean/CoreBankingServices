using AccountOperations.Domain;
using Microsoft.Extensions.Logging;
using SharedOperations.Domain;
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
        private readonly ICustomerResources _customerResources;

        public AccountService(ILogger<AccountService> logger, IAccountUnitOfWork unitOfWork, ICustomerResources customerResources)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _customerResources = customerResources;
        }


        public async Task Add(Account account)
        {
            try
            {
                bool customerExist = await _customerResources.IsExist(account.CustomerIdentity);
                if (!customerExist)
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
