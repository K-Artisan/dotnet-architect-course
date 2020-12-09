using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Zhaoxi.AspNetCore3_1.Interface;
using Zhaoxi.AspNetCore3_1.Service;
using Zhaoxi.AspNetCore31.Demo.Utility;
using Zhaoxi.AspNetCore31.Demo.Utility.Middleware;

namespace Zhaoxi.AspNetCore31.Demo
{
    /// <summary>
    /// 就是配置下，请求该如何处理
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }


        /// <summary>
        /// 初始化，最早执行且只执行一次的
        /// 给IOC容器增加映射关系，
        /// IServiceCollection--容器--就能完成DI--
        /// 
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddControllersWithViews(
                options =>
                {
                    options.Filters.Add<CustomExceptionFilterAttribute>();//全局注册
                    options.Filters.Add<CustomGlobalFilterAttribute>();
                });

            //services.AddScoped(typeof(CustomExceptionFilterAttribute));//不是直接new  而是容器生成 就可以自动注入了

            //只能构造函数注入--需要一个构造函数超集
            //services.AddTransient<ITestServiceA, TestServiceA>();//瞬时
            //services.AddSingleton<ITestServiceB, TestServiceB>();//单例
            //services.AddScoped<ITestServiceC, TestServiceC>();//作用域单例--一次请求一个实例
            ////作用域其实依赖于ServiceProvider（这个自身是根据请求的），跟多线程没关系
            //services.AddTransient<ITestServiceD, TestServiceD>();
            //services.AddTransient<ITestServiceE, TestServiceE>();
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            //containerBuilder.RegisterType<TestServiceE>().As<ITestServiceE>().SingleInstance();
            containerBuilder.RegisterModule<CustomAutofacModule>();
        }

        /// <summary>
        /// Http请求管道模型---就是Http请求被处理的步骤---Http请求是一段文本，被Kestrel解析得到HttpContext---然后被后台代码处理Request---返回Response---经由Kestrel回发到客户端
        /// 所谓管道，就是拿着HttpContext，经过多个步骤的加工，生成Response,就是管道
        /// Configure就是指定我们的代码如何去处理请求
        /// 
        /// 如果啥也不配置，但是默认是还有响应，是404
        /// 这个方法，叫请求级(所有请求生效)----页面级Home/Index
        /// 这个方法执行且只执行一次，是初始化
        /// 
        /// RequestDelegate是一个委托,接受一个HttpContext，执行操作，然后没有然后了
        /// IApplicationBuilder在Build之后 就是一个RequestDelegate
        /// Application--所谓管道，其实就应该是个RequestDelegate
        /// 
        /// 就像有一把手术刀，可以在流程的任意节点去扩展
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //任何请求都是响应这个， 真的是这个方法决定了请求怎么被响应
            //中断式中间件，直接停止了流程
            //app.Run(context => context.Response.WriteAsync("This is Hello World!"));

            #region Use中间件
            //app.Use(next =>
            //{
            //    Console.WriteLine("This is middleware 1");
            //    return new RequestDelegate(
            //        async context =>
            //        {
            //            await context.Response.WriteAsync("This is Hello World 1 start");
            //            await next.Invoke(context);
            //            await context.Response.WriteAsync("This is Hello World 1   end");
            //        });
            //});
            //app.Use(next =>
            //{
            //    Console.WriteLine("This is middleware 1.5");
            //    return new RequestDelegate(
            //        async context =>
            //        {
            //            await context.Response.WriteAsync("This is Hello World 1.5 start");
            //            await next.Invoke(context);
            //            await context.Response.WriteAsync("This is Hello World 1.5   end");
            //        });
            //});
            //app.Use(next =>
            //{
            //    Console.WriteLine("This is middleware 1.6");
            //    return new RequestDelegate(
            //        async context =>
            //        {
            //            await context.Response.WriteAsync("This is Hello World 1.6 start");
            //            await next.Invoke(context);
            //        });
            //});
            //app.Use(next =>
            //{
            //    Console.WriteLine("This is middleware 1.7");
            //    return new RequestDelegate(
            //        async context =>
            //        {
            //            await next.Invoke(context);
            //            await context.Response.WriteAsync("This is Hello World 1.7   end");
            //        });
            //});
            //app.Use(next =>
            //{
            //    Console.WriteLine("This is middleware 2");
            //    return new RequestDelegate(
            //        async context =>
            //        {
            //            await context.Response.WriteAsync("This is Hello World 2 start");
            //            await next.Invoke(context);
            //            await context.Response.WriteAsync("This is Hello World 2   end");
            //        });
            //});
            //app.Use(next =>
            //{
            //    Console.WriteLine("This is middleware 3");
            //    return new RequestDelegate(
            //        async context =>
            //        {
            //            await context.Response.WriteAsync("This is Hello World 3 start");
            //            //await next.Invoke(context);//最后这个没有执行Next
            //            await context.Response.WriteAsync("This is the one!");
            //            await context.Response.WriteAsync("This is Hello World 3   end");
            //        });
            //});
            #endregion

            #region middleware
            //Use很灵活，但是很复杂
            //终结式-- app.Use(_ => handler);---意味着没有下个步骤
            //app.Run(c => c.Response.WriteAsync("Hello World!"));

            //另外个use  可以不是request delegate
            //app.Use(async (context, next) =>//没有调用 next() 那就是终结点  跟Run一样
            //{
            //    await context.Response.WriteAsync("Hello World Use3  Again Again <br/>");
            //    //await next();
            //});
            //UseWhen可以对HttpContext检测后，增加处理环节;原来的流程还是正常执行的
            //app.UseWhen(context =>
            //{
            //    return context.Request.Query.ContainsKey("Name");
            //},
            //appBuilder =>
            //{
            //    appBuilder.Use(async (context, next) =>
            //    {
            //        await context.Response.WriteAsync("Hello World Use3 Again Again Again <br/>");
            //        //await next();
            //    });
            //});

            ////根据条件指定中间件 指向终结点，没有Next
            //app.Map("/Test", MapTest);
            //app.Map("/Eleven", a => a.Run(async context =>
            //{
            //    await context.Response.WriteAsync($"This is Advanced Eleven Site");
            //}));
            //app.MapWhen(context =>
            //{
            //    return context.Request.Query.ContainsKey("Name");
            //    //拒绝非chorme浏览器的请求  
            //    //多语言
            //    //把ajax统一处理
            //}, MapTest);
            //以上均为Use的封装，其实是为了熟悉的人方便，或者增加面试的复杂度

            ////UseMiddlerware 类--反射找
            //app.UseMiddleware<FirstMiddleWare>();
            //app.UseMiddleware<SecondMiddleWare>();
            //app.UseMiddleware<ThreeMiddleWare>();
            #endregion

            #region 环境参数
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
            #endregion

            #region 这些叫中间件，最终把请求交给MVC
            loggerFactory.AddLog4Net();
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot"))
            });
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            #endregion
        }
        private static void MapTest(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"Url is {context.Request.Path.Value}");
            });
        }
    }

}

//public delegate Task RequestDelegate(HttpContext context);
//public delegate async void RequestDelegate1(HttpContext context);



//public RequestDelegate Build()
//{
//    //声明一个RequestDelegate，默认处理步骤
//    RequestDelegate app = context =>
//    {
//        // If we reach the end of the pipeline, but we have an endpoint, then something unexpected has happened.
//        // This could happen if user code sets an endpoint, but they forgot to add the UseEndpoint middleware.
//        var endpoint = context.GetEndpoint();
//        var endpointRequestDelegate = endpoint?.RequestDelegate;
//        if (endpointRequestDelegate != null)
//        {
//            var message =
//                $"The request reached the end of the pipeline without executing the endpoint: '{endpoint.DisplayName}'. " +
//                $"Please register the EndpointMiddleware using '{nameof(IApplicationBuilder)}.UseEndpoints(...)' if using " +
//                $"routing.";
//            throw new InvalidOperationException(message);
//        }
//        context.Response.StatusCode = 404;
//        return Task.CompletedTask;
//    };


//    foreach (var component in _components.Reverse())//集合反转 123进来  321执行--
//    {
//        //Func<RequestDelegate,RequestDelegate> 是321的顺序
//        app = component(app);
//        //404--->middleware3-->middleware2-->middleware1  最终的结果Application其实就是middleware1
//    }

//    return app;
//}