using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.Web.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BolindersBil.Web
{
    public class Startup
    {
        IConfiguration _configuration;
        public Startup(IConfiguration conf)
        {
            _configuration = conf;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Configuration for DB connection.
            var conn = _configuration.GetConnectionString("BolindersBil");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(conn));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            // To get access to the wwwroot files...
            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Start}/{action=Index}/{id?}");
            });

            //Seed.FillIfEmpty(ctx);
        }
    }
}
