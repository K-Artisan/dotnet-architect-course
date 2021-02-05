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
            services.AddScoped<DbContext, JDDbContext>();//�������׻����ǰ��ҷ�װ��һ��
            services.AddScoped<IUserService, UserService>();

            //���ע��û�гɹ�--ע����û����ģ����캯��Ҳֻ��֧�ֲ����ͺã�����ע��ĵط�����дDbContext
            services.AddDbContext<JDDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("JDDbConnection"));
            });


            services.AddTransient<ITestServiceA, TestServiceA>();//˲ʱ
            services.AddSingleton<ITestServiceB, TestServiceB>();//����
            services.AddScoped<ITestServiceC, TestServiceC>();//��������--һ������һ��ʵ��
            //��������ʵ������ServiceProvider����������Ǹ�������ģ��������߳�û��ϵ
            services.AddTransient<ITestServiceD, TestServiceD>();
            services.AddTransient<ITestServiceE, TestServiceE>();

            #region jwtУ��
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//�Ƿ���֤Issuer
                    ValidateAudience = true,//�Ƿ���֤Audience
                    ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                    ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                    ValidAudience = this.Configuration["audience"],//Audience
                    ValidIssuer = this.Configuration["issuer"],//Issuer���������ǰ��ǩ��jwt������һ��
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["SecurityKey"])),//�õ�SecurityKey
                    //AudienceValidator = (m, n, z) =>
                    //{
                    //    return m != null && m.FirstOrDefault().Equals(this.Configuration["audience"]);
                    //},//�Զ���У����򣬿����µ�¼��֮ǰ����Ч
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
            app.UseAuthentication();//ע�������һ�䣬������֤
            #endregion

            app.UseRouting();

            app.UseAuthorization();
            Console.WriteLine(this.Configuration["ip"]);
            Console.WriteLine(this.Configuration["port"]);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //ʵ������ʱִ�У���ִֻ��һ��
            this.Configuration.ConsulRegist();
        }
    }
}
