using AccountOperations.Application;
using AccountOperations.Domain.Entity;
using AccountTransactionService.Handler;
using Microsoft.AspNetCore.Mvc;
using SharedOperations.Domain;

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
            Result<Unit, Error> result = await _accountService.Add(account);

            var resourceUrl = $"/accounts/{account.Number}";

            return ResultHandler.HandleResultCreated(result, resourceUrl);
        }

        [HttpGet("{accountNumber}")]
        public IActionResult Get([FromRoute] string accountNumber)
        {
            Result<Account?, Error> result = _accountService.Get(accountNumber);

            return ResultHandler.HandleResult(result);
        }

        [HttpPut("{accountNumber}")]
        public async Task<IActionResult> Update([FromRoute] string accountNumber, [FromBody] Account account)
        {
            Result<Unit, Error> result = await _accountService.Update(accountNumber, account);

            return ResultHandler.HandleResult(result);
        }

        [HttpDelete("{accountNumber}")]
        public IActionResult Delete([FromRoute] string accountNumber)
        {
            Result<Unit, Error> result = _accountService.Delete(accountNumber);

            return ResultHandler.HandleResult(result);
        }
    }
}
