using Microsoft.Extensions.Configuration;

using System.IO;

namespace Orm.Orm.Framework
{
    public class ConfigurationManager
    {
        private static string _sqlConnectionString = null;
        public static string SqlConnectionString
        {
            get
            {
                return _sqlConnectionString;
            }
        }

        static ConfigurationManager()
        {
            var builder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json");

            IConfigurationRoot configuration = builder.Build();
            _sqlConnectionString = configuration["connectionString"];

        }
    }
}
