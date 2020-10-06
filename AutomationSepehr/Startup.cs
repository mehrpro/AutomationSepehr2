using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutomationSepehr.DataModelLayer;
using AutomationSepehr.DataModelLayer.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutomationSepehr
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
            //DataBase Service
            services.AddDbContext<ApplicationDbContext>(
                optionsAction: optionsBuilder => optionsBuilder.UseSqlServer(Configuration.GetConnectionString("AutomationConnString"),
                    sqlServerOptionsAction: contextOptionsBuilder => contextOptionsBuilder.MigrationsAssembly("AutomationSepehr.DataModelLayer")));

            //Identity Service
            services.AddIdentity<ApplicationUsers, ApplicationRoles>().AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //AdminArea
                endpoints.MapAreaControllerRoute(
                    name: "AdminArea",
                    areaName: "AdminArea",
                    pattern: "AdminArea/{controller=UserManager}/{action=Index}/{id?}");
                //UserArea
                endpoints.MapAreaControllerRoute(
                     name: "UserArea",
                     areaName: "UserArea",
                     pattern: "UserArea/{controller=UserHome}/{action=Index}/{id?}");


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=LoginActionResult}/{id?}");
            });
        }
    }
}
