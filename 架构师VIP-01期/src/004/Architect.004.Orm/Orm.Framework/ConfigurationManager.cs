using Microsoft.Extensions.Configuration;

using System.IO;
using System.Linq;

namespace Orm.Orm.Framework
{
    public class ConfigurationManager
    {
        static ConfigurationManager()
        {
            var builder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json");

            IConfigurationRoot configuration = builder.Build();
            _SqlConnectionStringWrite = configuration["ConnectionStrings:Write"];
            _SqlConnectionStringRead = configuration.GetSection("ConnectionStrings").GetSection("Read").GetChildren().Select(s => s.Value).ToArray();

        }

        private static string _SqlConnectionStringWrite = null;
        public static string SqlConnectionStringWrite
        {
            get
            {
                return _SqlConnectionStringWrite;
            }
        }

        private static string[] _SqlConnectionStringRead = null;
        public static string[] SqlConnectionStringRead
        {
            get
            {
                return _SqlConnectionStringRead;
            }
        }
    }
}
