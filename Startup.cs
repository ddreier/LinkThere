using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using LinkThere.Models;
using System.IO;
using Microsoft.AspNet.StaticFiles;
using Microsoft.AspNet.Http;

namespace LinkThere
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddEntityFramework()
                .AddSqlite()
                .AddDbContext<LinkThereContext>(options =>
                {
                    options.UseSqlite("Data Source=" + Configuration["DbPath"]);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            string adminRoute = Configuration["AdminRoute"].ToString();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "admin",
                    template: adminRoute + "/{action=Index}/{id?}",
                    defaults: new { controller = "Admin" }
                );
                // wildcard route from https://github.com/aspnet/Mvc/issues/3084
                routes.MapRoute(
                    name: "links",
                    template: "{*anything}",
                    defaults: new { controller = "Link", action = "Get" },
                    constraints: new { anything = @"^(.*)?$" }
                );
                //routes.MapRoute(
                //    name: "default",
                //    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => Microsoft.AspNet.Hosting.WebApplication.Run<Startup>(args);
    }
}
