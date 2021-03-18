using AnotherBlog.Infra.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace AnotherBlog.Infra.Ioc
{
    public static class BlogDataConfig
    {
        public static void AddBlogDataConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            var migrationsAssembly = typeof(BlogContext).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<BlogContext>(options =>
            {
                options.UseMySQL(configuration.GetConnectionString("BlogConnection"), contextBuilder => {
                    contextBuilder.MigrationsAssembly(migrationsAssembly);
                });
            });
        }
    }
}
