using AccountOperations.Domain;
using AccountOperations.Domain.Entity;
using AccountOperations.Domain.Errors;
using AccountOperations.Domain.Generator;
using Microsoft.Extensions.Logging;
using SharedOperations.Domain;
using SharedOperations.Domain.Services;
using SharedOperations.Errors;

namespace AccountOperations.Application
{
    public class DefaultAccountService : IAccountService
    {

        private readonly ILogger<DefaultAccountService> _logger;
        private readonly IAccountUnitOfWork _unitOfWork;
        private readonly ICustomerResources _customerResources;
        private readonly IAccountNumberGenerator _accountNumberGenerator;

        public DefaultAccountService(ILogger<DefaultAccountService> logger, 
            IAccountUnitOfWork unitOfWork, 
            ICustomerResources customerResources,
            IAccountNumberGenerator accountNumberGenerator)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _customerResources = customerResources;
            _accountNumberGenerator = accountNumberGenerator;
        }


        public async Task<Result<Unit, Error>> Add(Account account)
        {
            try
            {
                bool customerExist = await _customerResources.IsExist(account.CustomerIdentity);
                if (!customerExist)
                {
                    return CustomerErrors.NotFound;
                }

                account.Number = _accountNumberGenerator.GenerateAccountNumber();

                _unitOfWork.Account.Add(account);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on create account.");
                throw;
            }
        }

        public Result<Account, Error> Get(long accountNumber)
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

        public async Task<Result<Unit, Error>> Update(long accountNumber, Account account)
        {
            try
            {
                account.Number = accountNumber;

                bool customerExist = await _customerResources.IsExist(account.CustomerIdentity);
                if (!customerExist)
                {
                    return CustomerErrors.NotFound;
                }

                _unitOfWork.Account.Update(account);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on update account.");
                throw;
            }
        }

        public Result<Unit, Error> Delete(long accountNumber)
        {
            try
            {
                Account account = Get(accountNumber).Value;
                if (account is null)
                {
                    return AccountErrors.NotFound;
                }
                account.State = 0;
                _unitOfWork.Account.Update(account);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on delete account.");
                throw;
            }
        }
    }
}
