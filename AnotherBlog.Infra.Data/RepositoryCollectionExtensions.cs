using AnotherBlog.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace AnotherBlog.Infra.Data
{
    public static class RepositoryCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection serviceCollection)
        {
            const string endsWith = "Repository";
            var repositorys = typeof(Repository<>).Assembly.GetTypes().Where(t => t.Name.EndsWith(endsWith) && t.GetInterfaces().Any(i => i.Name.EndsWith(endsWith))).ToList();
            if (repositorys.Count > 0)
            {
                foreach (var (item, i) in from item in repositorys from i in item.GetInterfaces() select (item, i))
                {
                    if (i.Name.EndsWith(endsWith))
                    {
                        serviceCollection.AddScoped(i, item);
                    }
                }
            }
            return serviceCollection;
        }
    }
}
