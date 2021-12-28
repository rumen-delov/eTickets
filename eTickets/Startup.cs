using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data;
using eTickets.Data.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eTickets
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
            // DbContext configuration
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));

            // Services configuration

            // .NET dependency injection system has 3 major lifetimes:
            // Singleton - services created once throughout the application, the singleton objects are the same for every object and every request
            // Scoped - services created once per request within the scope, the scope objects are the same within a request, but different across different requests 
            // Transient - services are created each time they are requested, the transient objects are always different and a new instance is provided to every controller and every service
            services.AddScoped<IActorsService, ActorsService>();

            // Included by the framework 
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // Seed database
            AppDbInitializer.Seed(app); // app is an IApplicationBuilder argument in the current method
        }
    }
}
