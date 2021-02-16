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
             
            #region 全局注册过滤器  
            //3. 全局注册：对所有的Api生效
            services.AddMvc(option =>
            {
                //option.Filters.Add(typeof(CustomActionFilterAttribute));
                option.Filters.Add(typeof(CustomGlobalActionFilterAttribute),order:9999);
                option.Filters.Add(typeof(CustomExceptionFilterAttribute));
            });
            #endregion


            services.AddControllers();

            #region 注册Swagger服务 
            services.AddSwaggerGen(s =>
            {
                #region 注册 Swagger
                s.SwaggerDoc("V1", new OpenApiInfo()
                {
                    Title = "test",
                    Version = "version-01",
                    Description = "朝夕教育Api学习"
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

            #region 使用Swagger中间件
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
