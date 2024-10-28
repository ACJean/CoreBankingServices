using CustomerOperations.Application.Dto;
using CustomerOperations.Application.Mapper;
using CustomerOperations.Domain;
using CustomerOperations.Domain.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SharedOperations.Domain;
using SharedOperations.Domain.Validator;
using SharedOperations.Errors;

namespace CustomerOperations.Application.Service
{
    public class DefaultCustomerService : ICustomerService
    {

        private readonly ILogger<DefaultCustomerService> _logger;
        private readonly ICustomerUnitOfWork _unitOfWork;
        private readonly IServiceProvider _serviceProvider;

        public DefaultCustomerService(ILogger<DefaultCustomerService> logger, ICustomerUnitOfWork unitOfWork, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _serviceProvider = serviceProvider;
        }

        public Result<Unit, Error> Add(CustomerCreateDto customerDto)
        {
            try
            {
                Customer customer = customerDto.ToEntity();

                var resultSetPhoneNumber = customer.SetPhoneNumber(customerDto.PhoneNumber, _serviceProvider.GetService<IPhoneNumberValidator>());
                if (resultSetPhoneNumber.Error is not null)
                {
                    return resultSetPhoneNumber.Error;
                }
                var resultSetPassword = customer.SetPassword(customerDto.Password, _serviceProvider.GetService<IPasswordValidator>());
                if (resultSetPassword.Error is not null)
                {
                    return resultSetPassword.Error;
                }
                customer.Enable();

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
                _logger.LogError(ex, "{Message}", "Error on create customer.");
                throw;
            }
        }

        public Result<CustomerDto, Error> Get(string identityNumber)
        {
            try
            {
                Customer customer = _unitOfWork.Customers.GetByIdentityNumber(identityNumber);
                CustomerDto customerDto = customer.ToDto();

                return customerDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message}", "Error on get customer.");
                throw;
            }
        }

        public Result<Unit, Error> Update(string identityNumber, CustomerUpdateDto customerDto)
        {
            try
            {
                Customer customer = _unitOfWork.Customers.GetByIdentityNumber(identityNumber);
                if (customer is null) return CustomerErrors.NotFound;

                customerDto.CopyToEntity(ref customer);

                var resultSetPhoneNumber = customer.SetPhoneNumber(customerDto.PhoneNumber, _serviceProvider.GetService<IPhoneNumberValidator>());
                if (resultSetPhoneNumber.Error is not null)
                {
                    return resultSetPhoneNumber.Error;
                }

                _unitOfWork.BeginTransaction();

                _unitOfWork.Customers.Update(customer);
                _unitOfWork.Persons.Update(customer);

                _unitOfWork.Commit();

                return Unit.Value;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.LogError(ex, "{Message}", "Error on update customer.");
                throw;
            }
        }

        public Result<Unit, Error> Delete(string identityNumber)
        {
            try
            {
                Customer customer = _unitOfWork.Customers.GetByIdentityNumber(identityNumber);
                if (customer is null) return CustomerErrors.NotFound;
                customer.Disable();
                _unitOfWork.Customers.Update(customer);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message}", "Error on delete customer.");
                throw;
            }
        }

    }
}
