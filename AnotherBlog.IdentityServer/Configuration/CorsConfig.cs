using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AnotherBlog.IdentityServer.Configuration
{
    public static class CorsConfig
    {
        public static void AddCorsPolicyConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddCors(options => options.AddPolicy("CorsPolicy",
               builder =>
               {
                   builder.AllowAnyOrigin();
                   builder.AllowAnyMethod();
                   builder.AllowAnyHeader();
               }));
        }

        public static void UseCorsPolicy(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseCors("CorsPolicy");
        }
    }
}
