using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Threading.Tasks;

namespace AnotherBlog.Infra.ConsulRegister
{
    public class HealthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _healthPath;

        public HealthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _healthPath = configuration["Consul:HealthPath"];
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path == _healthPath)
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
