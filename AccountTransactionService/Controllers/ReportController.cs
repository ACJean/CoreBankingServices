using Microsoft.AspNetCore.Mvc;

namespace AccountTransactionService.Controllers
{

    [ApiController]
    [Route("/reportes")]
    public class ReportController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get([FromQuery] string startDate, [FromQuery] string endDate)
        {
            return Ok();
        }

    }
}
