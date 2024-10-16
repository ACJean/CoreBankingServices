using AccountOperations.Application;
using AccountOperations.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace AccountTransactionService.Controllers
{
    [ApiController]
    [Route("/accounts")]
    public class AccountController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Account account)
        {
            await _accountService.Add(account);

            var resourceUrl = $"/accounts/{account.Number}";

            return Created(resourceUrl, null);
        }

        [HttpGet("{accountNumber}")]
        public IActionResult Get([FromRoute] string accountNumber)
        {
            return Ok(_accountService.Get(accountNumber));
        }

        [HttpPut("{accountNumber}")]
        public async Task<IActionResult> Update([FromRoute] string accountNumber, [FromBody] Account account)
        {
            await _accountService.Update(accountNumber, account);

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
