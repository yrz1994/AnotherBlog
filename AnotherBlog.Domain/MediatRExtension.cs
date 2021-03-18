using AnotherBlog.Domain.Core.Bus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AnotherBlog.Domain
{
    public static class MediatRExtension
    {
        public static IServiceCollection AddMediatR(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(typeof(MediatRExtension));
            serviceCollection.AddScoped<IMemoryBus, MemoryBus>();
            return serviceCollection;
        }
    }
}
