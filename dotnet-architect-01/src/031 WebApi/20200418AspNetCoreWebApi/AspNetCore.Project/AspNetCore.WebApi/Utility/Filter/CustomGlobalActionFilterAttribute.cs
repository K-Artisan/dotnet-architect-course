using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.WebApi.Utility.Filter
{

    public class CustomGlobalActionFilterAttribute : Attribute, IActionFilter
    {

        private ILogger<CustomGlobalActionFilterAttribute> _Logger = null;

        public CustomGlobalActionFilterAttribute(ILogger<CustomGlobalActionFilterAttribute> logger)
        {
            _Logger = logger;
        }

        /// <summary>
        /// 方法执行后
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        { 
            _Logger.LogInformation($"this is CustomGlobalActionFilterAttribute.OnActionExecuted");

        }

        /// <summary>
        /// 方法执行前
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _Logger.LogInformation($"this is CustomGlobalActionFilterAttribute.OnActionExecuting");
        }
    }
     

   
}
