using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VegaIoTApi.AppServices;
using VegaIoTApi.Data;

namespace VegaIoTApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

#if RELEASE
            services.AddDbContext<VegaApiDBContext>
                (options => options.UseHerokuPostgres(Configuration["DATABASE_URL"]));
#elif DEBUG
            services.AddDbContext<VegaApiDBContext>
                (options => options.UseHerokuPostgres("postgres://ezmmidgliiyfhn:85d1a46cf86d68be37cd1a5e679d028adb64662dfad31ff9dd23acdcb6b221ae@ec2-23-21-91-183.compute-1.amazonaws.com:5432/dd42vpimo3vofo"));
#endif

            services.AddControllers();

            services.AddSingleton<IVegaApiCommunicator, VegaApiCommunicator>(provider =>
           {
               var par = Configuration.GetVegaConnectionParameters("DefaultConnection");
               return new VegaApiCommunicator(par.URL, par.Login, par.Password);
           });

            services.AddAppServices();
            services.AddRepositories();
            services.AddHostedServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseWebSockets();
        }
    }
}