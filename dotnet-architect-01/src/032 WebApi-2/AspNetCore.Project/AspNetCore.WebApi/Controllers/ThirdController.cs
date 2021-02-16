using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Dal;
using AspNetCore.WebApi.Utility.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[CrossDomainFilterAttribute]
    public class ThirdController : ControllerBase
    {
        [HttpGet]
        [Route("GetCrossDaminData")]
        public string GetCrossDaminData()
        {
            //base.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*"); 
            return Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    id = 123,
                    Name = "Richard",
                    Description = "这个Api 就是专门用测试跨域问题。。"
                });
        }

        [HttpGet]
        [Route("GetCrossDaminData1")]
        public string GetCrossDaminData1()
        {
            //base.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    id = 123,
                    Name = "开塔克撞火车",
                    Description = "这个Api 就是专门用测试跨域问题。。"
                });
        }


        [HttpGet]
        [Route("GetCrossDaminData2")]
        //[AllowAnonymous]
        public string GetCrossDaminData2()
        {
            base.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    id = 123,
                    Name = "开塔克撞火车",
                    Description = "这个Api 就是专门用测试跨域问题。。"
                });
        }
    }
}