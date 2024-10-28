using AccountOperations.Application;
using AccountOperations.Domain.Entity;
using AccountTransactionService.Handler;
using Microsoft.AspNetCore.Mvc;
using SharedOperations.Domain;

namespace AccountTransactionService.Controllers
{

    [ApiController]
    [Route("/reports")]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IReportService _reportService;

        public ReportController(ILogger<ReportController> logger, IReportService reportService)
        {
            _logger = logger;
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string startDate, [FromQuery] string endDate)
        {
            Result<IEnumerable<AccountMovement>, Error> result = await _reportService.GetAccountMovements(startDate, endDate);

            return ResultHandler.HandleResult(result);
        }

    }
}
