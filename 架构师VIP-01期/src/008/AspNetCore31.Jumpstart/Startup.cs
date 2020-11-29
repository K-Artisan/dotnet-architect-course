using AspNetCore31.Interface;
using AspNetCore31.Jumpstart.Autofaces;
using AspNetCore31.Jumpstart.Middlewares;
using AspNetCore31.Jumpstart.Utiltiy;
using AspNetCore31.Service;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore31.Jumpstart
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
            services.AddControllersWithViews(options => { //定义全局的Filters， 所有的控制器、操作都使用
                //options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                options.Filters.Add(typeof(CustomGlobalFilterAttribute));
            });

            services.AddScoped(typeof(CustomExceptionFilterAttribute));


            /*          services.AddTransient<ITestServiceA, TestServiceA>();//瞬时,每次使用都创建一个实例
                        services.AddSingleton<ITestServiceB, TestServiceB>();//单例
                        services.AddScoped<ITestServiceC, TestServiceC>();//作用域单例,一次请求只创建一个实例
                        //作用域其实依赖于ServiceProvider（这个自身是根据请求的），跟多线程没关系
                        services.AddTransient<ITestServiceD, TestServiceD>();
                        services.AddTransient<ITestServiceE, TestServiceE>();
            */
        }

        //Asp.Net框架自身会调用该方法
        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            // Add any Autofac modules or registrations.
            // This is called AFTER ConfigureServices so things you
            // register here OVERRIDE things registered in ConfigureServices.
            //
            // You must have the call to AddAutofac in the Program.Main
            // method or this won't be called.

            //containerBuilder.RegisterType<TestServiceA>().As<ITestServiceA>().SingleInstance();
            containerBuilder.RegisterModule<CustomAutofacModule>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddLog4Net();

            app.Use(async (context, next) =>
            {
                // Do work that doesn't write to the Response.
                await next.Invoke();
                // Do logging or other work that doesn't write to the Response.
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseRequestCulture(); //自定义中间件

            #region app.Use(next =>

            app.Use(next =>
            {
                Console.WriteLine("This is middleware 1");
                return new RequestDelegate(
                    async context =>
                    {
                        await next.Invoke(context);
                    });
            });

            app.Use(next =>
            {
                Console.WriteLine("This is middleware 2");
                return new RequestDelegate(
                    async context =>
                    {
                        await next.Invoke(context);
                    });
            });
            #endregion

            #region app.Use(async (context, next) =>

            app.Use(async (context, next) =>
            {
                Console.WriteLine("app.Use(async (context, next) => This is middleware 1--start");
                await next.Invoke();
                Console.WriteLine("app.Use(async (context, next) => This is middleware 1--end");
            });

            app.Use(async (context, next) =>
            {
                Console.WriteLine("app.Use(async (context, next) => This is middleware 2--start");
                await next.Invoke();
                Console.WriteLine("app.Use(async (context, next) => This is middleware 2--end");
            });

            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
