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
using VegaIoTApi.Migrations;
using VegaIoTApi.Data;
using Microsoft.AspNetCore;

namespace VegaIoTApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<VegaApiDBContext>();
                context.Database.Migrate();
            }
            host.Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
             WebHost.CreateDefaultBuilder(args)
                    .UseUrls("http://*:" + Environment.GetEnvironmentVariable("PORT"))
                    .UseStartup<Startup>();
    }
}
