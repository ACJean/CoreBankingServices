using AccountOperations.Application;
using AccountOperations.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace AccountTransactionService.Controllers
{

    [ApiController]
    [Route("/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
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
