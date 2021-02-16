using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.WebApi.Utility.Filter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace AspNetCore.WebApi
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
            // IServiceCollection serviceDescriptors = new ServiceCollection();
            //serviceDescriptors.BuildServiceProvider(); 
            services.AddSingleton<CustomActionFilterAttribute>(); 
            services.AddSingleton<CustomActionNewFilterAttribute>();
             
            #region ȫ��ע�������  
            //3. ȫ��ע�᣺�����е�Api��Ч
            services.AddMvc(option =>
            {
                //option.Filters.Add(typeof(CustomActionFilterAttribute));
                option.Filters.Add(typeof(CustomGlobalActionFilterAttribute),order:9999);
                option.Filters.Add(typeof(CustomExceptionFilterAttribute));
            });
            #endregion


            services.AddControllers();

            #region ע��Swagger���� 
            services.AddSwaggerGen(s =>
            {
                #region ע�� Swagger
                s.SwaggerDoc("V1", new OpenApiInfo()
                {
                    Title = "test",
                    Version = "version-01",
                    Description = "��Ϧ����Apiѧϰ"
                });
                #endregion 
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region ʹ��Swagger�м��
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/V1/swagger.json", "test1");
            });
            #endregion


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
