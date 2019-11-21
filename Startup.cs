using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvendoTest_v1.Search.Models.Elasticsearch;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Swashbuckle.AspNetCore.Swagger;

namespace EvendoTest_v1.Search
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

            services.AddSwaggerGen(c =>
            {
              c.SwaggerDoc("v1", new Info
              {
                Title = "EvenDo API",
                Version = "v1",
                Description = "EvenDo API (ASP.NET Core Web API with Docker)",
                TermsOfService = "None",
                Contact = new Contact
                {
                  Name = "EvenDo Test",
                  Url = ""
                }
              });

              //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
              //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
              //c.IncludeXmlComments(xmlPath);

            });


            var connectionString = Configuration.GetConnectionString("elasticsearch");
            
            // 1. Register NEST ElasticClient
            var settings = new ConnectionSettings(new Uri(connectionString))
                .DefaultIndex("items");

            services.AddSingleton(settings);

            services.AddScoped(s =>
            {
                var connectionSettings = s.GetRequiredService<ConnectionSettings>();
                var client = new ElasticClient(connectionSettings);

                return client;
            });

            
            // 2. Register Items Loader
            services.AddScoped<ItemsUtil>();
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
              c.SwaggerEndpoint("/swagger/v1/swagger.json", "EvenDo API V1");
              c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}