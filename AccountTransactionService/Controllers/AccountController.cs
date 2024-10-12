using AccountOperations.Application;
using AccountOperations.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AccountTransactionService.Controllers
{
    [ApiController]
    [Route("/cuentas")]
    public class AccountController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        private readonly AccountService _accountService;

        public AccountController(ILogger<AccountController> logger, AccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Account account)
        {
            await _accountService.Add(account);

            return Ok();
        }

        [HttpGet("{accountNumber}")]
        public IActionResult Get([FromRoute] string accountNumber)
        {
            return Ok(_accountService.Get(accountNumber));
        }

        [HttpPut("{accountNumber}")]
        public IActionResult Update([FromRoute] string accountNumber, [FromBody] Account account)
        {
            _accountService.Update(account);

            return Ok();
        }

        [HttpDelete("{accountNumber}")]
        public IActionResult Delete([FromRoute] string accountNumber)
        {
            _accountService.Delete(accountNumber);

            return Ok();
        }
    }
}
