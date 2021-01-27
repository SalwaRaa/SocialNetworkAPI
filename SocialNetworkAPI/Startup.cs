using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SocialNetworkAPI.Repositories;
using SocialNetworkAPI.Services;


namespace SocialNetworkAPI
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
            services.AddControllers().AddNewtonsoftJson();

            //Tegister the swagger generator defining 1 or more swagger documents
            services.AddSwaggerGen(e =>
            {
                e.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "SocialNetwork API",
                    Description = "API for creating and liking posts.",
                    Contact = new OpenApiContact
                    {
                        Name = "Salwa Abdul-Rahman",
                        Email = "salwaa.abdulrahman@gmail.com",
                        Url = new Uri("https://github.com/SalwaRaa")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                e.IncludeXmlComments(xmlPath);
               
            });

            //(DI container) when it wants a IuserRepo give it a UserRepo
            services.AddSingleton<IUserRepository, DictionaryUserRepository>();
            //when it wants a IpostRepo give it a postRepo
            services.AddSingleton<IPostRepository, DictionaryPostRepository>();
            services.AddSingleton<IPostService, PostService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();
            // will be used when genereting its gränssnitt
            app.UseSwaggerUI(e =>
            {
                e.SwaggerEndpoint("/swagger/v1/swagger.json", "SocialNetwork API v1");
            }
            );
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
