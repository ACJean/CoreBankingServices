using AccountOperations.Application;
using AccountOperations.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AccountTransactionService.Controllers
{

    [ApiController]
    [Route("/reportes")]
    public class ReportController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string startDate, [FromQuery] string endDate)
        {
            IEnumerable<AccountMovement> accountMovements = await _reportService.GetAccountMovements(startDate, endDate);

            return Ok(accountMovements);
        }

    }
}
