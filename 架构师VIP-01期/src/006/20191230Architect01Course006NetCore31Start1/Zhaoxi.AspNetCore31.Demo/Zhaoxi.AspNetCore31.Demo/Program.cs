using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Zhaoxi.AspNetCore31.Demo
{
    /// <summary>
    /// ����̨
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();//������kestrel
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)//����ָ��kestrel
                                           //.ConfigureLogging(loggingBuilder =>
                                           //    {
                                           //        loggingBuilder.AddLog4Net();//��Ҫ�����ļ�
                                           //    })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();//��Startup��������MVC
                });
    }
}
