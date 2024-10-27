using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CustomerProfileService.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var messages = context.ModelState
                    .SelectMany(message => message.Value?.Errors ?? new ModelErrorCollection())
                    .Select(error => error.ErrorMessage)
                    .ToList();

                context.Result = new BadRequestObjectResult(new { validations = messages });
            }
        }
    }
}
