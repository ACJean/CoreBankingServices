using CustomerOperations.Application.Dto;
using CustomerOperations.Domain.Entity;

namespace CustomerOperations.Application.Mapper
{
    public static class CustomerMapper
    {

        public static Customer ToEntity(this CustomerCreateDto customerDto)
        {
            if (customerDto is null) return null;

            Customer customer = new()
            {
                IdentityNumber = customerDto.IdentityNumber ?? default,
                Name = customerDto.Name ?? default,
                Age = customerDto.Age == 0 ? default : customerDto.Age,
                Gender = customerDto.Gender ?? default,
                Address = customerDto.Address ?? default
            };

            return customer;
        }

        public static CustomerDto ToDto(this Customer customer)
        {
            if (customer is null) return null;

            CustomerDto customerDto = new()
            {
                IdentityNumber = customer.IdentityNumber ?? default,
                Name = customer.Name ?? default,
                Age = customer.Age == 0 ? default : customer.Age,
                Gender = customer.Gender ?? default,
                Address = customer.Address ?? default,
                PhoneNumber = customer.PhoneNumber ?? default,
                State = customer.State == 0 ? default : customer.State
            };

            return customerDto;
        }

        public static bool CopyToEntity(this CustomerUpdateDto customerDto, ref Customer customer)
        {
            if (customerDto is null) return false;

            customer.Name = customerDto.Name ?? default;
            customer.Age = customerDto.Age == 0 ? default : customerDto.Age;
            customer.Gender = customerDto.Gender ?? default;
            customer.Address = customerDto.Address ?? default;

            return true;
        }

    }
}
