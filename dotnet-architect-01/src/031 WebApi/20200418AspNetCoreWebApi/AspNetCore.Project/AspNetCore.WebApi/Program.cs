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
                  .ConfigureLogging(logging => //支持IOC  控制反转
                  {
                      logging.AddFilter("System", LogLevel.Warning);  //忽略系统日志
                      logging.AddFilter("Microsoft", LogLevel.Warning);   //忽略系统日志
                      logging.AddLog4Net("ConfigFile/log4net.Config");
                  })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
