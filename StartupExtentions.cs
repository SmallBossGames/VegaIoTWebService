using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VegaIoTApi.AppServices;
using VegaIoTApi.Data;

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
            return services;
        }

        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services;
        }

        public static string GetVegaConnectionUrl(this IConfiguration configuration, string key)
            => configuration.GetSection("VegaConnectionUrls").GetValue<string>(key);
    }
}