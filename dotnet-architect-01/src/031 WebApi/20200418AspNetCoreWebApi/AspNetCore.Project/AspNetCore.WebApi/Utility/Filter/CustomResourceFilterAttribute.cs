using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.WebApi.Utility.Filter
{
    public class CustomResourceFilterAttribute : Attribute, IResourceFilter
    {
        private static Dictionary<string, object> dictionaryCache = new Dictionary<string, object>();
         
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //在这里缓存数据
            //Console.WriteLine("this is CustomResourceFilterAttribute.OnResourceExecuted");
            string key = context.HttpContext.Request.Path.ToString();
            ObjectResult objectResult = context.Result as ObjectResult;
            dictionaryCache[key] = objectResult; //这里就存入缓存
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            //定义缓存的Key
            string key = context.HttpContext.Request.Path.ToString();
            if (dictionaryCache.ContainsKey(key))
            {
                context.Result = dictionaryCache[key] as ObjectResult; //context.Result 短路器
            }
        }
    }
}
