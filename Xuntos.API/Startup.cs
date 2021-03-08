using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xuntos.API.Data;
using Newtonsoft.Json.Serialization;
using Xuntos.API.Filter;

namespace Xuntos.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiDbContext>(opt => opt.UseSqlServer
            (Configuration.GetConnectionString("ApiConnection")));

            services.AddControllers().AddNewtonsoftJson(s => {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            });

            // AutoMapper automaically handles DTO mapping
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Mock version for testing purposes
            //services.AddScoped<IApiRepo, MockApiRepo>();

            // Db implementation
            services.AddScoped<ICaseRepo, SqlCaseRepo>();
            services.AddScoped<ICompanyRepo, SqlCompanyRepo>();
            services.AddScoped<ITechniqueRepo, SqlTechniqueRepo>();
            services.AddScoped<ICaseTechniqueRepo, SqlCaseTechniqueRepo>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.DocumentFilter<JsonPatchDocumentFilter>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Xuntos .NET Core REST API");
                c.RoutePrefix = string.Empty;
                c.DefaultModelsExpandDepth(-1);
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
