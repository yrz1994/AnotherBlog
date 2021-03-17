using AnotherBlog.Application.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace AnotherBlog.IdentityServer.Filter
{
    public class ModelValidateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(e => e.Errors);
                if (errors != null)
                {
                    context.Result = new OkObjectResult(new BaseResponse(System.Net.HttpStatusCode.BadRequest, string.Join(";", errors.Select(e => e.ErrorMessage))));
                }
            }
        }
    }
}
