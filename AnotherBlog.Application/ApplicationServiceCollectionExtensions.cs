using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace AnotherBlog.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection serviceCollection)
        {
            const string servicesEndsWith = "AppService";
            var localServices = typeof(ApplicationServiceCollectionExtensions).Assembly.GetTypes().Where(t => t.Name.EndsWith(servicesEndsWith) && t.GetInterfaces().Any(i => i.Name.EndsWith(servicesEndsWith))).ToList();
            if (localServices.Count > 0)
            {
                foreach (var item in localServices)
                {
                    foreach (var i in item.GetInterfaces())
                    {
                        serviceCollection.AddScoped(i, item);
                    }
                }
            }
            return serviceCollection;
        }
    }
}
