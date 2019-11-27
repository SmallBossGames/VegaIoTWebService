using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using VegaIoTApi.Data;
using Microsoft.AspNetCore;

namespace VegaIoTApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
#if RELEASE
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<VegaApiDBContext>();
                context.Database.Migrate();
            }
#endif
            host.Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
             WebHost.CreateDefaultBuilder(args)
#if RELEASE
                    .UseUrls("http://*:" + Environment.GetEnvironmentVariable("PORT"))
#endif
                    .UseStartup<Startup>();
    }
}
