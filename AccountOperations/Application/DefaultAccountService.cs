using AccountOperations.Domain;
using AccountOperations.Domain.Entity;
using AccountOperations.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using SharedOperations.Domain.Exceptions;
using SharedOperations.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AccountOperations.Application
{
    public class DefaultAccountService : IAccountService
    {

        private readonly ILogger<DefaultAccountService> _logger;
        private readonly IAccountUnitOfWork _unitOfWork;
        private readonly ICustomerResources _customerResources;

        public DefaultAccountService(ILogger<DefaultAccountService> logger, IAccountUnitOfWork unitOfWork, ICustomerResources customerResources)
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
                    throw new CustomerNotFoundException("Customer not found.");
                }

                account.Number = Guid.NewGuid()
                    .ToString()
                    .Replace("-", "")
                    .Substring(0, 10);

                _unitOfWork.Account.Add(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on create account.");
                throw;
            }
        }

        public Account? Get(string accountNumber)
        {
            try
            {
                return _unitOfWork.Account.GetById(accountNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on get account.");
                throw;
            }
        }

        public async Task Update(string accountNumber, Account account)
        {
            try
            {
                account.Number = accountNumber;

                bool customerExist = await _customerResources.IsExist(account.CustomerIdentity);
                if (!customerExist)
                {
                    throw new InvalidOperationException("Customer not found.");
                }

                _unitOfWork.Account.Update(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on update account.");
                throw;
            }
        }

        public void Delete(string accountNumber)
        {
            try
            {
                Account? account = Get(accountNumber) ?? throw new InvalidOperationException($"Account not found.");
                account.State = 0;
                _unitOfWork.Account.Update(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on delete account.");
                throw;
            }
        }
    }
}
