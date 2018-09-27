using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.Data.DataAccess;
using BolindersBil.Models;
using BolindersBil.Repositories;
using BolindersBil.Web.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BolindersBil.Web
{
    public class Startup
    {
        // IConfiguration is what you use to get info from the appsettings.json file.
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
            // Register a service for the DB.
            services.AddDbContext<ApplicationDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(conn));

            // Register a service for VehicleRepository
            services.AddTransient<IVehicleRepository, VehicleRepository>();

            // Register a service for Identity.
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Register the service so the components can access information.
            services.AddTransient<IVehicleRepository, VehicleRepository>();
            services.AddTransient<IIdentitySeeder, IdentitySeeder>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext ctx, IIdentitySeeder identitySeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            // To get access to the wwwroot files...
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Start}/{action=Index}/{id?}");

                // Incase we need routing in navbar
                //routes.MapRoute(
                //  name: null,
                //  template: "",
                //  defaults: new { controller = "Home", action = "Home" }
                //  );
            });

            var runIdentitySeed = Task.Run(async () => await identitySeeder.CreateAdminAccountIfEmpty()).Result;

            //identitySeeder.CreateAdminAccountIfEmpty();
            Seed.FillIfEmpty(ctx);
            
        }
    }
}
