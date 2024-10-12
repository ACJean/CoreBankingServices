using CustomerOperations.Domain;
using CustomerOperations.Infrastructure.EF.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOperations.Application
{
    public class CustomerService
    {

        private readonly ILogger<CustomerService> _logger;
        private readonly ICustomerUnitOfWork _unitOfWork;

        public CustomerService(ILogger<CustomerService> logger, ICustomerUnitOfWork unitOfWork)
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
                    throw new InvalidOperationException($"A customer with IdentityNumber {customer.IdentityNumber} already exists.");
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
