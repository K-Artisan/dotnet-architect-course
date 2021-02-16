using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.WebApi.Utility.Filter
{

    public class CustomActionFilterAttribute :Attribute, IActionFilter
    {

        private ILogger<CustomActionFilterAttribute> _Logger = null;

        public CustomActionFilterAttribute(ILogger<CustomActionFilterAttribute> logger)
        {
            _Logger = logger;
        }

        /// <summary>
        /// 方法执行后
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var result = context.Result;
            ObjectResult objectResult = result as ObjectResult;
            var resultLog = $"{DateTime.Now} 调用 {context.RouteData.Values["action"]} api 完成；执行结果：{Newtonsoft.Json.JsonConvert.SerializeObject(objectResult.Value)}";
            _Logger.LogInformation(resultLog);

        }

        /// <summary>
        /// 方法执行前
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionLog = $"{DateTime.Now} 开始调用 {context.RouteData.Values["action"]} api；参数为：{Newtonsoft.Json.JsonConvert.SerializeObject(context.ActionArguments)}";
            _Logger.LogInformation(actionLog);
        }
    }

    public class CustomAsyncActionFilterAttribute : Attribute, IAsyncActionFilter
    {
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            throw new NotImplementedException();
        }
    }
     
    /// <summary>
    /// ActionFilterAttribute ：框架已经封装了一个全套给我们
    /// </summary>
    public class CustomActionFilterAttribute01:ActionFilterAttribute 
    {

        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    base.OnActionExecuted(context);
        //}

    }

   
}
