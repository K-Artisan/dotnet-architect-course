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
    /// ���������£��������δ���
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }


        /// <summary>
        /// ��ʼ��������ִ����ִֻ��һ�ε�
        /// ��IOC��������ӳ���ϵ��
        /// IServiceCollection--����--�������DI--
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
                    options.Filters.Add<CustomExceptionFilterAttribute>();//ȫ��ע��
                    options.Filters.Add<CustomGlobalFilterAttribute>();
                });

            //services.AddScoped(typeof(CustomExceptionFilterAttribute));//����ֱ��new  ������������ �Ϳ����Զ�ע����

            //ֻ�ܹ��캯��ע��--��Ҫһ�����캯������
            //services.AddTransient<ITestServiceA, TestServiceA>();//˲ʱ
            //services.AddSingleton<ITestServiceB, TestServiceB>();//����
            //services.AddScoped<ITestServiceC, TestServiceC>();//��������--һ������һ��ʵ��
            ////��������ʵ������ServiceProvider����������Ǹ�������ģ��������߳�û��ϵ
            //services.AddTransient<ITestServiceD, TestServiceD>();
            //services.AddTransient<ITestServiceE, TestServiceE>();
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            //containerBuilder.RegisterType<TestServiceE>().As<ITestServiceE>().SingleInstance();
            containerBuilder.RegisterModule<CustomAutofacModule>();
        }

        /// <summary>
        /// Http����ܵ�ģ��---����Http���󱻴���Ĳ���---Http������һ���ı�����Kestrel�����õ�HttpContext---Ȼ�󱻺�̨���봦��Request---����Response---����Kestrel�ط����ͻ���
        /// ��ν�ܵ�����������HttpContext�������������ļӹ�������Response,���ǹܵ�
        /// Configure����ָ�����ǵĴ������ȥ��������
        /// 
        /// ���ɶҲ�����ã�����Ĭ���ǻ�����Ӧ����404
        /// ���������������(����������Ч)----ҳ�漶Home/Index
        /// �������ִ����ִֻ��һ�Σ��ǳ�ʼ��
        /// 
        /// RequestDelegate��һ��ί��,����һ��HttpContext��ִ�в�����Ȼ��û��Ȼ����
        /// IApplicationBuilder��Build֮�� ����һ��RequestDelegate
        /// Application--��ν�ܵ�����ʵ��Ӧ���Ǹ�RequestDelegate
        /// 
        /// ������һ�������������������̵�����ڵ�ȥ��չ
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //�κ���������Ӧ����� ������������������������ô����Ӧ
            //�ж�ʽ�м����ֱ��ֹͣ������
            //app.Run(context => context.Response.WriteAsync("This is Hello World!"));

            #region Use�м��
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
            //            //await next.Invoke(context);//������û��ִ��Next
            //            await context.Response.WriteAsync("This is the one!");
            //            await context.Response.WriteAsync("This is Hello World 3   end");
            //        });
            //});
            #endregion

            #region middleware
            //Use�������Ǻܸ���
            //�ս�ʽ-- app.Use(_ => handler);---��ζ��û���¸�����
            //app.Run(c => c.Response.WriteAsync("Hello World!"));

            //�����use  ���Բ���request delegate
            //app.Use(async (context, next) =>//û�е��� next() �Ǿ����ս��  ��Runһ��
            //{
            //    await context.Response.WriteAsync("Hello World Use3  Again Again <br/>");
            //    //await next();
            //});
            //UseWhen���Զ�HttpContext�������Ӵ�����;ԭ�������̻�������ִ�е�
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

            ////��������ָ���м�� ָ���ս�㣬û��Next
            //app.Map("/Test", MapTest);
            //app.Map("/Eleven", a => a.Run(async context =>
            //{
            //    await context.Response.WriteAsync($"This is Advanced Eleven Site");
            //}));
            //app.MapWhen(context =>
            //{
            //    return context.Request.Query.ContainsKey("Name");
            //    //�ܾ���chorme�����������  
            //    //������
            //    //��ajaxͳһ����
            //}, MapTest);
            //���Ͼ�ΪUse�ķ�װ����ʵ��Ϊ����Ϥ���˷��㣬�����������Եĸ��Ӷ�

            ////UseMiddlerware ��--������
            //app.UseMiddleware<FirstMiddleWare>();
            //app.UseMiddleware<SecondMiddleWare>();
            //app.UseMiddleware<ThreeMiddleWare>();
            #endregion

            #region ��������
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

            #region ��Щ���м�������հ����󽻸�MVC
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
//    //����һ��RequestDelegate��Ĭ�ϴ�����
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


//    foreach (var component in _components.Reverse())//���Ϸ�ת 123����  321ִ��--
//    {
//        //Func<RequestDelegate,RequestDelegate> ��321��˳��
//        app = component(app);
//        //404--->middleware3-->middleware2-->middleware1  ���յĽ��Application��ʵ����middleware1
//    }

//    return app;
//}