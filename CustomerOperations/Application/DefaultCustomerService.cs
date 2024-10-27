using CustomerOperations.Domain;
using CustomerOperations.Domain.Entity;
using Microsoft.Extensions.Logging;
using SharedOperations.Domain;
using SharedOperations.Errors;

namespace CustomerOperations.Application
{
    public class DefaultCustomerService : ICustomerService
    {

        private readonly ILogger<DefaultCustomerService> _logger;
        private readonly ICustomerUnitOfWork _unitOfWork;

        public DefaultCustomerService(ILogger<DefaultCustomerService> logger, ICustomerUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public Result<Unit, Error> Add(Customer customer)
        {
            try
            {
                if (Get(customer.IdentityNumber).Value is not null)
                {
                    return CustomerErrors.AlreadyExist;
                }

                _unitOfWork.BeginTransaction();

                customer.PersonId = _unitOfWork.Persons.Add(customer);

                _unitOfWork.Customers.Add(customer);

                _unitOfWork.Commit();

                return Unit.Value;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.LogError(ex, "Error on create customer.");
                throw;
            }
        }

        public Result<Customer, Error> Get(string identityNumber)
        {
            try
            {
                return _unitOfWork.Customers.GetByIdentityNumber(identityNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on get customer.");
                throw;
            }
        }

        public Result<Unit, Error> Update(Customer customer)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                _unitOfWork.Customers.Update(customer);
                _unitOfWork.Persons.Update(customer);

                _unitOfWork.Commit();

                return Unit.Value;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.LogError(ex, "Error on update customer.");
                throw;
            }
        }

        public Result<Unit, Error> Delete(string identityNumber)
        {
            try
            {
                Customer customer = Get(identityNumber).Value;
                if (customer is null) return CustomerErrors.NotFound;
                customer.State = 0;
                _unitOfWork.Customers.Update(customer);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on delete customer.");
                throw;
            }
        }

    }
}
