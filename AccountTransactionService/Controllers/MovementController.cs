using AccountOperations.Application;
using AccountOperations.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace AccountTransactionService.Controllers
{
    [ApiController]
    [Route("/movements")]
    public class MovementController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        private readonly IMovementsService _movementsService;

        public MovementController(ILogger<AccountController> logger, IMovementsService movementsService)
        {
            _logger = logger;
            _movementsService = movementsService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Movements movement)
        {
            _movementsService.Add(movement);

            var resourceUrl = $"/movements/{movement.Id}";

            return Created(resourceUrl, null);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            return Ok(_movementsService.Get(id));
        }

    }
}
