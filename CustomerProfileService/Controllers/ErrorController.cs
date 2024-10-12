using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CustomerProfileService.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult HandleError()
        {
            // Puedes personalizar el contenido de la respuesta de error
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            if (exception != null)
            {
                // Aquí puedes registrar el error y luego devolver la respuesta adecuada
                // Por ejemplo, loguearlo usando Serilog u otro sistema de logging
            }

            // Ejemplo: devolver un 500 por defecto
            return Problem(
                detail: exception?.Message,
                statusCode: StatusCodes.Status500InternalServerError,
                title: "An unexpected error occurred.");
        }
    }
}
