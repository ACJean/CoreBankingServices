using Microsoft.AspNetCore.Mvc;
using SharedOperations.Domain;

namespace AccountTransactionService.Handler
{
    public class ResultHandler
    {

        public static IActionResult HandleResult<TValue, TError>(Result<TValue, TError> result)
        {
            if (result.Error is not null)
            {
                return new BadRequestObjectResult(result.Error);
            }

            if (result.Value is Unit) return new OkResult();
            else return new OkObjectResult(result.Value);
        }

        public static IActionResult HandleResultCreated<TValue, TError>(Result<TValue, TError> result, string location)
        {
            if (result.Error is not null)
            {
                return new BadRequestObjectResult(result.Error);
            }

            return new CreatedResult(location, null);
        }

    }
}
