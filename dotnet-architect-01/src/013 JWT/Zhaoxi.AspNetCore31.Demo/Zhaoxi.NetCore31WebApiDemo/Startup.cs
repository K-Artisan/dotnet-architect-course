using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Zhaoxi.AspNetCore3_1.Interface;
using Zhaoxi.AspNetCore3_1.Service;
using Zhaoxi.EntityFrameworkCore31.Model;
using Zhaoxi.NetCore31WebApiDemo.Utility;

namespace Zhaoxi.NetCore31WebApiDemo
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
            services.AddControllers();
            services.AddScoped<DbContext, JDDbContext>();//下面这套还不是把我封装了一下
            services.AddScoped<IUserService, UserService>();

            //这个注入没有成功--注入是没问题的，构造函数也只是支持参数就好，错在注入的地方不能写DbContext
            services.AddDbContext<JDDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("JDDbConnection"));
            });


            services.AddTransient<ITestServiceA, TestServiceA>();//瞬时
            services.AddSingleton<ITestServiceB, TestServiceB>();//单例
            services.AddScoped<ITestServiceC, TestServiceC>();//作用域单例--一次请求一个实例
            //作用域其实依赖于ServiceProvider（这个自身是根据请求的），跟多线程没关系
            services.AddTransient<ITestServiceD, TestServiceD>();
            services.AddTransient<ITestServiceE, TestServiceE>();

            #region jwt校验
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidAudience = this.Configuration["audience"],//Audience
                    ValidIssuer = this.Configuration["issuer"],//Issuer，这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["SecurityKey"])),//拿到SecurityKey
                    //AudienceValidator = (m, n, z) =>
                    //{
                    //    return m != null && m.FirstOrDefault().Equals(this.Configuration["audience"]);
                    //},//自定义校验规则，可以新登录后将之前的无效
                };
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

            app.UseHttpsRedirection();

            #region jwt
            app.UseAuthentication();//注意添加这一句，启用验证
            #endregion

            app.UseRouting();

            app.UseAuthorization();
            Console.WriteLine(this.Configuration["ip"]);
            Console.WriteLine(this.Configuration["port"]);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //实例启动时执行，且只执行一次
            this.Configuration.ConsulRegist();
        }
    }
}
