using AnotherBlog.Infra.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace AnotherBlog.IdentityServer.Configuration
{
    public static class UserDataConfig
    {
        public static void AddUserDataConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<UserContext>(options =>
            {
                options.UseMySQL(configuration.GetConnectionString("IdentityConnection"), contextBuilder => {
                    contextBuilder.MigrationsAssembly(migrationsAssembly);
                });
            });
        }
    }
}
