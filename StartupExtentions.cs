using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
    }
}