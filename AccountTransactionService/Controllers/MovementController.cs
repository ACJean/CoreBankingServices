using AccountOperations.Application;
using AccountOperations.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AccountTransactionService.Controllers
{
    [ApiController]
    [Route("/movimientos")]
    public class MovementController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        private readonly MovementsService _movementsService;

        public MovementController(ILogger<AccountController> logger, MovementsService movementsService)
        {
            _logger = logger;
            _movementsService = movementsService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Movements movement)
        {
            _movementsService.Add(movement);

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            return Ok(_movementsService.Get(id));
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Movements movement)
        {
            _movementsService.Update(movement);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _movementsService.Delete(id);

            return Ok();
        }

    }
}
