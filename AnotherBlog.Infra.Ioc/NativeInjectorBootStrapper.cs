using AnotherBlog.Application;
using AnotherBlog.Domain;
using AnotherBlog.Infra.Data;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AnotherBlog.Infra.Ioc
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapperConfiguration();

            services.AddRepository();

            services.AddMediatR();

            services.AddApplicationService();
         
        }
    }
}
