using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EvoteWebServer
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<EvoteWebServer.Models.EvoteWebServerContext>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<EvoteWebServer.Models.EvoteWebServerContext>();
                //context.Database.EnsureCreated();
            }
            //app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void seedData(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<EvoteWebServer.Models.EvoteWebServerContext>();
                var voters = new List<Models.Voter>{new Models.Voter{
                    userID = "12345",
                },
                new Models.Voter{
                    userID = "45678"
                }};

                var candidates = new List<Models.Candidate>{
                    new Models.Candidate{
                        Name = "buhari",
                        electiontype = Models.ElectionType.PRESIDENTIAL
                    },
                    new Models.Candidate{
                        Name = "Atiku",
                        electiontype = Models.ElectionType.PRESIDENTIAL
                    }
                };
            }
        }
    }
}
