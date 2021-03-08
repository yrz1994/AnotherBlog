using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Text;

namespace AnotherBlog.Infra.ConsulRegister
{
    public static class ConsulRegister
    {
        public static IApplicationBuilder AgentServiceRegister(this IApplicationBuilder app, IHostApplicationLifetime lifetime,
            IConfiguration configuration)
        {
            var scheme = configuration["scheme"];
            var ip = configuration["ip"];
            var port = configuration["port"];
            var serviceName = configuration["Consul:ServiceName"];
            var registrationId = GetRegistrationId();
            var consulClient = new ConsulClient(c =>
            {
                c.Address = new Uri(configuration["Consul:Address"]);
                c.Datacenter = configuration["Consul:Datacenter"];
            });

            lifetime.ApplicationStarted.Register(() =>
            {
                var healthCheck = new AgentServiceCheck
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(30),
                    Interval = TimeSpan.FromSeconds(10),
                    HTTP = $"{scheme}://{ip}:{port}{configuration["Consul:HealthPath"]}",
                    Timeout = TimeSpan.FromSeconds(5),
                    TLSSkipVerify = true
                };

                var registration = new AgentServiceRegistration
                {
                    Checks = new[] { healthCheck },
                    ID = registrationId,
                    Name = serviceName,
                    Address = ip,
                    Port = int.Parse(port),
                    Tags = null
                };
                consulClient.Agent.ServiceRegister(registration).Wait();
            });

            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registrationId).Wait();
            });

            app.UseMiddleware<HealthMiddleware>();

            return app;
        }

        private static string GetRegistrationId()
        {
            try
            {
                var basePath = Directory.GetCurrentDirectory();
                var folderPath = Path.Combine(basePath, "registrationid");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var path = Path.Combine(basePath, "registrationid", ".id");
                if (File.Exists(path))
                {
                    var lines = File.ReadAllLines(path, Encoding.UTF8);
                    if (lines.Length > 0 && !string.IsNullOrEmpty(lines[0]))
                    {
                        return lines[0];
                    }
                }
                var id = Guid.NewGuid().ToString();
                File.AppendAllLines(path, new[] { id });
                return id;
            }
            catch
            {
                return Guid.NewGuid().ToString();
            }
        }
    }
}
