using AccountOperations.Application;
using AccountOperations.Domain.Entity;
using AccountTransactionService.Handler;
using Microsoft.AspNetCore.Mvc;
using SharedOperations.Domain;

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
            Result<Unit, Error> result = _movementsService.Add(movement);

            var resourceUrl = $"/movements/{movement.Id}";

            return ResultHandler.HandleResultCreated(result, resourceUrl) ;
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            Result<Movements?, Error> result = _movementsService.Get(id);

            return ResultHandler.HandleResult(result);
        }

    }
}
