using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NginxController : ControllerBase
    {
        private readonly ILogger<FirstController> _logger;

        public NginxController(ILogger<FirstController> logger)
        {
            _logger = logger;
            _logger.LogInformation($"{nameof(FirstController)} 控制器被实例化~~");
        }

        private static int iCount = 0;
        private static string id = Guid.NewGuid().ToString("D");

        [HttpGet]
        [Route("GetString")]
        public string GetString()
        {
            var host = Request.Host.ToString();
            return Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                应用程序监听端口号 = $"this is {host}, Guid={id}",
                访问次数 = ++iCount
            });
        }

    }
}