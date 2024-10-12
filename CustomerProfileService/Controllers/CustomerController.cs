using CustomerOperations.Application;
using CustomerOperations.Domain;
using CustomerProfileService.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CustomerProfileService.Controllers
{
    [ApiController]
    [Route("/clientes")]
    public class CustomerController : ControllerBase
    {

        private readonly ILogger<CustomerController> _logger;
        private readonly CustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, CustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Customer customer)
        {
            _customerService.Add(customer);

            var resourceUrl = $"/clientes/{customer.IdentityNumber}";

            return Created(resourceUrl, null);
        }

        [HttpGet("{identityNumber}")]
        public IActionResult Get([FromRoute] string identityNumber)
        {
            return Ok(_customerService.Get(identityNumber));
        }

        [HttpPut("{identityNumber}")]
        public IActionResult Update([FromRoute] string identityNumber, [FromBody] Customer customer)
        {
            _customerService.Update(customer);

            return Ok();
        }

        [HttpDelete("{identityNumber}")]
        public IActionResult Delete([FromRoute] string identityNumber)
        {
            _customerService.Delete(identityNumber);

            return Ok();
        }
    }
}
