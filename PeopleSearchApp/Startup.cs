using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PeopleSearchApp.DataAccessLayer.DBContexts;
using System.IO;
using PeopleSearchApp.DataAccessLayer.Models;
using System;

namespace PeopleSearchApp
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            ConfigureInMemoryDBContext(services);
        }

        private void ConfigureInMemoryDBContext(IServiceCollection services)
        {
            //Seed the in memory database. This could be done in OnModelCreating,
            //but that seems to only get called during migrations
            string currentDirectory = Directory.GetCurrentDirectory();
            string hanPhoto = Convert.ToBase64String(File.ReadAllBytes(currentDirectory + "\\Photos\\han-solo.jpg"));
            string lukePhoto = Convert.ToBase64String(File.ReadAllBytes(currentDirectory + "\\Photos\\luke-skywalker.jpeg"));
            string chewyPhoto = Convert.ToBase64String(File.ReadAllBytes(currentDirectory + "\\Photos\\chewy.jpg"));

            var hanId = int.MaxValue - 1;
            var lukeId = int.MaxValue - 2;
            var chewyId = int.MaxValue - 3;

            var people = new Person[]
                {
                    new Person()
                    {
                        Id = hanId,
                        FirstName = "Han",
                        LastName = "Solo",
                        Age = 25,
                        StreetAddress = "111 Space Smuggler Road",
                        City = "Millennium Falcon",
                        State = "Space",
                        ZipCode = 555556,
                        Photograph = hanPhoto
                    },
                    new Person()
                    {
                        Id = lukeId,
                        FirstName = "Luke",
                        LastName = "Skywalker",
                        Age = 20,
                        StreetAddress = "12 Sandhut lane",
                        City = "",
                        State = "Tatooine",
                        ZipCode = 555555,
                        Photograph = lukePhoto
                    },
                    new Person()
                    {
                        Id = chewyId,
                        FirstName = "Chew",
                        LastName = "Bacca",
                        Age = 20,
                        StreetAddress = "1 Wookie BLVD",
                        City = "HERRNNGGGHH",
                        State = "Kashyyyk",
                        ZipCode = 555557,
                        Photograph = chewyPhoto
                    }
                };

            DbContextOptionsBuilder<PeopleDBContext> options = new DbContextOptionsBuilder<PeopleDBContext>();
            options.UseInMemoryDatabase(Configuration.GetConnectionString("DevConnection"));
            PeopleDBContext context = new PeopleDBContext(options.Options);

            context.AddRange(people);
            context.SaveChanges();

            services.AddSingleton(context);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
