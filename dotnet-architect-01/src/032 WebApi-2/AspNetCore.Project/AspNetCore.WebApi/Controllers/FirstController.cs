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
    public class FirstController : ControllerBase
    { 
        private readonly ILogger<FirstController> _logger;

        public FirstController(ILogger<FirstController> logger)
        {
            _logger = logger;
            _logger.LogInformation($"{nameof(FirstController)} 控制器被实例化~~");
        }

        [HttpGet]
        [Route("GetString")]
        public string GetString()
        {
            _logger.LogInformation("GetString 被调用");

            return "欢迎大家来到架构班的Vip课程";
        }


        [HttpGet]
        [Route("GetInt")]
        public int GetInt(int i)
        {
            _logger.LogInformation($"GetString 被调用，参数：{i}");
            return i;
        }


        [HttpGet]
        [Route("GetJson")]
        public string GetJson(int id, string name)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                Id = id,
                Name = name
            });
        }


    }
}