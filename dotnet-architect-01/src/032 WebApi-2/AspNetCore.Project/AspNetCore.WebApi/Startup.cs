using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.WebApi.Utility.Filter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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


            #region JWT鉴权授权
            //1.Nuget引入程序包：Microsoft.AspNetCore.Authentication.JwtBearer 
            //services.AddAuthentication();//禁用  
            var validAudience = this.Configuration["audience"];
            var validIssuer = this.Configuration["issuer"];
            var securityKey = this.Configuration["SecurityKey"];
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  //默认授权机制名称；                                      
                     .AddJwtBearer(options =>
                     {
                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidateIssuer = true,//是否验证Issuer
                             ValidateAudience = true,//是否验证Audience
                             ValidateLifetime = true,//是否验证失效时间
                             ValidateIssuerSigningKey = true,//是否验证SecurityKey
                             ValidAudience = validAudience,//Audience
                             ValidIssuer = validIssuer,//Issuer，这两项和前面签发jwt的设置一致  表示谁签发的Token
                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))//拿到SecurityKey
                             //AudienceValidator = (m, n, z) =>
                             //{ 
                             //    return m != null && m.FirstOrDefault().Equals(this.Configuration["audience"]);
                             //},//自定义校验规则，可以新登录后将之前的无效 
                         };
                     });
            #endregion

            #region 支持跨域  所有的Api都支持跨域
            services.AddCors(option => option.AddPolicy("AllowCors", _build => _build.AllowAnyOrigin().AllowAnyMethod()));
            #endregion


            #region 全局注册过滤器  
            //3. 全局注册：对所有的Api生效
            services.AddMvc(option =>
            {
                //option.Filters.Add(typeof(CustomActionFilterAttribute));
                option.Filters.Add(typeof(CustomGlobalActionFilterAttribute),order:9999);
                option.Filters.Add(typeof(CustomExceptionFilterAttribute));
                //option.Filters.Add(typeof(CrossDomainFilterAttribute));
                
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

            #region 通过中间件来支持鉴权授权
            app.UseAuthentication(); //告诉框架 要使用权限认证
            #endregion


            app.UseRouting();

            #region 支持跨域
            app.UseCors("AllowCors");
            #endregion

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
