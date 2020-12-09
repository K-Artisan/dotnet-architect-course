using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Zhaoxi.AspNetCore31.Demo
{
    /// <summary>
    /// 控制台
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();//启动个kestrel
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)//就是指定kestrel
                                           //.ConfigureLogging(loggingBuilder =>
                                           //    {
                                           //        loggingBuilder.AddLog4Net();//需要配置文件
                                           //    })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())//设置工厂来替换实例
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();//靠Startup来串起来MVC
                });
    }
}
