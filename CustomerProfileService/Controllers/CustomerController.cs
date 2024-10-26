using CustomerOperations.Application;
using CustomerOperations.Domain.Entity;
using CustomerProfileService.Handler;
using Microsoft.AspNetCore.Mvc;
using SharedOperations.Domain;

namespace CustomerProfileService.Controllers
{
    [ApiController]
    [Route("/customers")]
    public class CustomerController : ControllerBase
    {

        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Customer customer)
        {
            Result<Unit, Error> result = _customerService.Add(customer);

            var resourceUrl = $"/customers/{customer.IdentityNumber}";

            return ResultHandler.HandleResultCreated(result, resourceUrl);
        }

        [HttpGet("{identityNumber}")]
        public IActionResult Get([FromRoute] string identityNumber)
        {   
            Result<Customer?, Error> result = _customerService.Get(identityNumber);

            return ResultHandler.HandleResult(result);
        }

        [HttpPut("{identityNumber}")]
        public IActionResult Update([FromRoute] string identityNumber, [FromBody] Customer customer)
        {

            Result<Unit, Error> result = _customerService.Update(customer);

            return ResultHandler.HandleResult(result);
        }

        [HttpDelete("{identityNumber}")]
        public IActionResult Delete([FromRoute] string identityNumber)
        {
            Result<Unit, Error> result = _customerService.Delete(identityNumber);

            return ResultHandler.HandleResult(result);
        }
    }
}
