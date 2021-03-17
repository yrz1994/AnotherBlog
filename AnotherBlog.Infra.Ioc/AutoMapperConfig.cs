using AnotherBlog.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace AnotherBlog.Infra.Ioc
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(DomainToResponseMappingProfile), typeof(RequestToCommandMappingProfile));
        }
    }
}
