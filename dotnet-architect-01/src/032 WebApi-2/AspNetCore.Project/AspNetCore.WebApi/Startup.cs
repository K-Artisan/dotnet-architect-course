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


            #region JWT��Ȩ��Ȩ
            //1.Nuget����������Microsoft.AspNetCore.Authentication.JwtBearer 
            //services.AddAuthentication();//����  
            var validAudience = this.Configuration["audience"];
            var validIssuer = this.Configuration["issuer"];
            var securityKey = this.Configuration["SecurityKey"];
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  //Ĭ����Ȩ�������ƣ�                                      
                     .AddJwtBearer(options =>
                     {
                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidateIssuer = true,//�Ƿ���֤Issuer
                             ValidateAudience = true,//�Ƿ���֤Audience
                             ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                             ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                             ValidAudience = validAudience,//Audience
                             ValidIssuer = validIssuer,//Issuer���������ǰ��ǩ��jwt������һ��  ��ʾ˭ǩ����Token
                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))//�õ�SecurityKey
                             //AudienceValidator = (m, n, z) =>
                             //{ 
                             //    return m != null && m.FirstOrDefault().Equals(this.Configuration["audience"]);
                             //},//�Զ���У����򣬿����µ�¼��֮ǰ����Ч 
                         };
                     });
            #endregion

            #region ֧�ֿ���  ���е�Api��֧�ֿ���
            services.AddCors(option => option.AddPolicy("AllowCors", _build => _build.AllowAnyOrigin().AllowAnyMethod()));
            #endregion


            #region ȫ��ע�������  
            //3. ȫ��ע�᣺�����е�Api��Ч
            services.AddMvc(option =>
            {
                //option.Filters.Add(typeof(CustomActionFilterAttribute));
                option.Filters.Add(typeof(CustomGlobalActionFilterAttribute),order:9999);
                option.Filters.Add(typeof(CustomExceptionFilterAttribute));
                //option.Filters.Add(typeof(CrossDomainFilterAttribute));
                
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

            #region ͨ���м����֧�ּ�Ȩ��Ȩ
            app.UseAuthentication(); //���߿�� Ҫʹ��Ȩ����֤
            #endregion


            app.UseRouting();

            #region ֧�ֿ���
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
