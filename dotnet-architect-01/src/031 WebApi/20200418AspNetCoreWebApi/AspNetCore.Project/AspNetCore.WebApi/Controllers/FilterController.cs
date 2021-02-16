using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Dal;
using AspNetCore.WebApi.Utility.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[CustomIOCFilterFactoryAttribute(typeof(CustomActionFilterAttribute))]//2.控制器注册 对当前控制器下的所有Api都生效
    [TypeFilter(typeof(CustomControllerActionFilterAttribute),Order =8888)]
    public class FilterController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public FilterController(ILogger<WeatherForecastController> logger)
        {
            throw new Exception("Richard 老师就是要异常一下~"); //这里就是控制器构造函数发生异常了
            _logger = logger;
            _logger.LogInformation($"{typeof(FirstController)} 被构造..... ");
        }

        [Route("GetDateTime")]
        [HttpGet]
        [CustomResourceFilterAttribute]
        public string GetDateTime()
        {
            return $"this is Zhaoxi Framwwork  {DateTime.Now}";
        }

        [HttpGet]
        [Route("GetInfoByParamter")]
        //1.方法注入  仅对当前方法生效
        //[CustomActionFilterAttribute()] //不能传递变量 
        //[TypeFilter(typeof(CustomActionFilterAttribute))] //支持注入
        //[ServiceFilter(typeof(CustomActionFilterAttribute))]
        //[CustomIOCFilterFactoryAttribute(typeof(CustomActionFilterAttribute))]
        public string GetInfoByParamter(int id, string Name)
        {
            return $"this is Id={id},Name={Name}";
        }

        [HttpGet]
        [Route("GetJSONInfo")]
        [ServiceFilter(typeof(CustomActionNewFilterAttribute),Order =10)] //Scope:  按照Order的值 从小到大执行
        public string GetJSONInfo(int id, string Name)
        { 
            _logger.LogInformation($"&&&&&&&&&&&&&&&&&&&&GetJSONInfo 被执行&&&&&&&&&&&&&&&&&&&&&&&&&&");
            return Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                Id = 123,
                Name = "Ricahrd"
            });
        }

        [HttpGet]
        [Route("ToError")] 
        public string ToError()
        { 
            {
                UserService userService = new UserService();

                userService.GetString();
            }
            int i = 0;
            int j = 10;
            int x = j / i;
            return "你好";
        }


    }
}