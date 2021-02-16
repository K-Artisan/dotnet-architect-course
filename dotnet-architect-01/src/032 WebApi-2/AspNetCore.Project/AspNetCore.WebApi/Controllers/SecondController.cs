using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SecondController : ControllerBase
    {

        private ILogger<SecondController> _Logger = null;

        public SecondController(ILogger<SecondController> logger)
        {
            this._Logger = logger;
            _Logger.LogInformation("SecondController 被构造。。。。");
        }

        [Route("GetInfo")]
        [HttpGet]
        public string GetInfo()
        {
            _Logger.LogInformation("SecondController.Get 被调用");
            return "这里是没有增加权限认证的";
        }

        [Route("Get")]
        [HttpGet]
        [Authorize]
        public string Get()
        {
            _Logger.LogInformation("SecondController.Get 被调用"); 
            //var claims = base.HttpContext.AuthenticateAsync().Result.Principal.Claims; 
            // var data=  claims.FirstOrDefault(); 
            //return Newtonsoft.Json.JsonConvert.SerializeObject(data);
            return "朝夕教育";
        }
    }
}