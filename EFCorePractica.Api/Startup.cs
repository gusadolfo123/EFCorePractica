using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EFdNorthWind.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EFCorePractica.Api
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

            // Configuracion Swagger
            services.AddSwaggerGen(config => config.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info()
            {
                Title = "EFdNorthwind"
            }));

            var filePath = Path.Combine(AppContext.BaseDirectory, "EFCorePractica.Api.xml");
            services.AddSwaggerGen(config => config.IncludeXmlComments(filePath));

            services.AddSingleton(typeof(ICategoryOperations), EFdNorthWind.BLL.OperarionsFactory.GetCategoryOperations());
            services.AddSingleton(typeof(ILogOperations), EFdNorthWind.BLL.OperarionsFactory.GetLogOperations());
            services.AddSingleton(typeof(IProductOperations), EFdNorthWind.BLL.OperarionsFactory.GetProductsOperations());

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
            app.UseHttpsRedirection();
            app.UseMvc();

            // Configuracion Swagger
            app.UseSwagger();
            app.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/v1/swagger.json", "EFdNorthwind API"));
        }
    }
}
