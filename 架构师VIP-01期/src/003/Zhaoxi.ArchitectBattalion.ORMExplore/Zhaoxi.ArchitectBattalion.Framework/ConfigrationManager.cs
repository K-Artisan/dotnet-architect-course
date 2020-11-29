using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Zhaoxi.ArchitectBattalion.Framework
{
    /// <summary>
    /// 固定读取根目录下面的appsettings.json
    /// </summary>
    public class ConfigrationManager
    {
        //有了IOC再去注入--容器单例
        static ConfigrationManager()
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

            IConfigurationRoot configuration = builder.Build();
            _SqlConnectionString = configuration["connectionString"];
        }
        private static string _SqlConnectionString = null;
        public static string SqlConnectionString
        {
            get
            {
                return _SqlConnectionString;
            }
        }
    }
}
