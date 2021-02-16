using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.WebApi.Utility.Filter
{

    public class CrossDomainFilterAttribute : Attribute, IActionFilter
    {

        private ILogger<CrossDomainFilterAttribute> _Logger = null;

        public CrossDomainFilterAttribute(ILogger<CrossDomainFilterAttribute> logger)
        {
            _Logger = logger;
        }

        /// <summary>
        /// 方法执行后
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {

            context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        /// <summary>
        /// 方法执行前
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //var actionLog = $"{DateTime.Now} 开始调用 {context.RouteData.Values["action"]} api；参数为：{Newtonsoft.Json.JsonConvert.SerializeObject(context.ActionArguments)}";
            //_Logger.LogInformation(actionLog);
        }
    }

     
}
