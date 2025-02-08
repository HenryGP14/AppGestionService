using Application.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Utility.Static;

namespace Api.Extensions
{
    public class ValidateClaimExtension : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Method == HttpMethod.Post.Method)
            {
                var identity = context.HttpContext.User.Identity as ClaimsIdentity;
                if (identity == null || !identity.Claims.Any(c => c.Type == ClaimTypes.Name))
                {
                    var response = new GenericResponse<object>
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status401Unauthorized,
                        StatusMessage = HttpStatus.MS_ERROR,
                        Message = "El claim no existe o es null"
                    };
                    context.Result = new JsonResult(response)
                    {
                        StatusCode = response.StatusCode
                    };
                    return;
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // No implementation needed for this example
        }
    }
}
