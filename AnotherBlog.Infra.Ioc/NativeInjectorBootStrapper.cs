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

            services.AddApplicationService();
        }
    }
}
