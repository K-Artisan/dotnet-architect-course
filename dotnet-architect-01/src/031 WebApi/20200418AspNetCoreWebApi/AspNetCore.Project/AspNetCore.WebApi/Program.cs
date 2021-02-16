using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AspNetCore.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                  .ConfigureLogging(logging => //֧��IOC  ���Ʒ�ת
                  {
                      logging.AddFilter("System", LogLevel.Warning);  //����ϵͳ��־
                      logging.AddFilter("Microsoft", LogLevel.Warning);   //����ϵͳ��־
                      logging.AddLog4Net("ConfigFile/log4net.Config");
                  })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
