using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace AnotherBlog.IdentityServer.Configuration
{
    public static class IdentityServerConfig
    {
        public static void AddIdentityServerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var connectionString = configuration.GetConnectionString("MySQLConnection");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            var identityBuilder = services.AddIdentityServer(
                      setting =>
                      {
                          setting.UserInteraction.LoginUrl = "~/account/login";
                          setting.UserInteraction.LogoutUrl = "~/account/logout";
                          setting.UserInteraction.ErrorUrl = "~/home/error";
                      }
                )
              // this adds the config data from DB (clients, resources)
              .AddConfigurationStore(options =>
              {
                  options.ConfigureDbContext = builder => builder.UseMySql(connectionString,
                      new MySqlServerVersion(new Version(8, 0, 23)), optionsBuilder => optionsBuilder
                          .CharSetBehavior(CharSetBehavior.NeverAppend)
                          .MigrationsAssembly(migrationsAssembly));
              })
              // this adds the operational data from DB (codes, tokens, consents)
              .AddOperationalStore(options =>
              {
                  options.ConfigureDbContext = builder => builder.UseMySql(connectionString, 
                      new MySqlServerVersion(new Version(8, 0, 23)), optionsBuilder => optionsBuilder
                        .CharSetBehavior(CharSetBehavior.NeverAppend)
                        .MigrationsAssembly(migrationsAssembly));
                  // this enables automatic token cleanup. this is optional.
                  options.EnableTokenCleanup = true;
                  options.TokenCleanupInterval = 3600;
              });
            identityBuilder.AddDeveloperSigningCredential();
            //identityBuilder.AddSigningCredential(new X509Certificate2(configuration["Certificates:Path"], configuration["Certificates:Password"]));
        }

        public static void InitIdentityServerDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in IdentityServerDBConfig.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in IdentityServerDBConfig.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    foreach (var resource in IdentityServerDBConfig.GetApiScopes())
                    {
                        context.ApiScopes.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in IdentityServerDBConfig.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
