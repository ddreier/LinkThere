using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using LinkThere.Models;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Http;

namespace LinkThere
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddDbContext<LinkThereContext>(options =>
                options.UseSqlite("Data Source=" + Configuration["DbPath"]));

            services.AddSingleton<IConfiguration>(Configuration);
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

            // Get some settings
            string baseUrl = Configuration["AppUrl"].ToString();
            string adminRoute = baseUrl + Configuration["AdminRoute"].ToString();

            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = baseUrl
            });


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "admin",
                    template: adminRoute.Substring(1) + "/{action=Index}/{id?}",
                    defaults: new { controller = "Admin" }
                );
                // wildcard route from https://github.com/aspnet/Mvc/issues/3084
                routes.MapRoute(
                    name: "links",
                    template: baseUrl.Substring(1) + "/{*anything}",
                    defaults: new { controller = "Link", action = "Get" },
                    constraints: new { anything = @"^(.*)?$" }
                );
                //routes.MapRoute(
                //    name: "default",
                //    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
