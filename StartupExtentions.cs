using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VegaIoTApi.AppServices;
using VegaIoTApi.Data;
using VegaIoTApi.Repositories;
using VegaIoTApi.Repositories.Interfaces;
using VegaIoTWebService.HostedServices;

namespace VegaIoTApi
{
    public static class StartupExtentions
    {
        public static DbContextOptionsBuilder UseHerokuPostgres(this DbContextOptionsBuilder options, string DatabseUrl)
        {
            var builder = new PostgreSqlConnectionStringBuilder(DatabseUrl)
            {
                Pooling = true,
                TrustServerCertificate = true,
                SslMode = SslMode.Require
            };

            options.UseNpgsql(builder.ConnectionString);

            return options;
        }

        public static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<VegaDeviceDbSync>();
            return services;
        }

        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<ITemperatureDeviceDataRepository, TemperatureDeviceDataRepository>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services;
        }

        public static VegaConnectionParameters GetVegaConnectionParameters
            (this IConfiguration configuration, string key)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var section = configuration.GetSection("VegaConnection").GetSection(key);
            return new VegaConnectionParameters
            (
                new Uri(section.GetValue<string>("URL")),
                section.GetValue<string>("Login"),
                section.GetValue<string>("Password")
            );
        }
    }

    public class VegaConnectionParameters
    {
        public VegaConnectionParameters(Uri URL, string login, string password)
        {
            this.URL = URL;
            Login = login;
            Password = password;
        }

        public Uri URL { get; }
        public string Login { get; }
        public string Password { get; }
    }
}