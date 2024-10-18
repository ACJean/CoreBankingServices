using CustomerOperations.Domain;
using CustomerOperations.Domain.Entity;
using Microsoft.Extensions.Logging;
using SharedOperations.Domain.Exceptions;

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

        public void Add(Customer customer)
        {
            try
            {
                if (Get(customer.IdentityNumber) != null)
                {
                    throw new CustomerAlreadyExistException($"A customer with IdentityNumber {customer.IdentityNumber} already exists.");
                }

                _unitOfWork.BeginTransaction();

                customer.PersonId = _unitOfWork.Persons.Add(customer);

                _unitOfWork.Customers.Add(customer);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();

                _logger.LogError(ex, "Error on create customer.");

                throw;
            }
        }

        public Customer? Get(string identityNumber)
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

        public void Update(Customer customer)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                _unitOfWork.Customers.Update(customer);
                _unitOfWork.Persons.Update(customer);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on update customer.");
                throw;
            }
        }

        public void Delete(string identityNumber)
        {
            try
            {
                Customer? customer = Get(identityNumber) ?? throw new InvalidOperationException($"Customer not found.");
                customer.State = 0;
                _unitOfWork.Customers.Update(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on delete customer.");
                throw;
            }
        }

    }
}
