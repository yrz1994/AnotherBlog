using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Threading.Tasks;

namespace AnotherBlog.ArticleAPI
{
    public static class UseHealthMiddleware
    {
        public static void UseHealth(this IApplicationBuilder app)
        {
            app.UseMiddleware<HealthMiddleware>();
        }
    }
    
    public class HealthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _healthPath = "/health";

        public HealthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            var healthPath = configuration["Consul:HealthPath"];
            if (!string.IsNullOrEmpty(healthPath))
            {
                _healthPath = healthPath;
            }
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path == this._healthPath)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                await httpContext.Response.WriteAsync("I'm OK!");
            }
            else
            {
                await _next(httpContext);
            }
        }
    }
}
