using CustomerOperations.Application.Dto;
using SharedOperations.Domain;

namespace CustomerOperations.Application.Service
{
    public interface ICustomerService
    {

        Result<Unit, Error> Add(CustomerCreateDto customerDto);
        Result<CustomerDto, Error> Get(string identityNumber);
        Result<Unit, Error> Update(string identityNumber, CustomerUpdateDto customerDto);
        Result<Unit, Error> Delete(string identityNumber);

    }
}
