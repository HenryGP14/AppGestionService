using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos.Response;
using Utility.Static;

namespace Api.Extensions
{
    public class ValidateRequestExtension : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Method == HttpMethod.Post.Method)
            {
                var requestBody = context.ActionArguments.Values.FirstOrDefault();
                if (requestBody == null)
                {
                    var response = new GenericResponse<object>
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        StatusMessage = HttpStatus.MS_ERROR,
                        Message = "El request no existe datos"
                    };
                    context.Result = new JsonResult(response)
                    {
                        StatusCode = response.StatusCode
                    };
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // No implementation needed for this example
        }
    }
}
