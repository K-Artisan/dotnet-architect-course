using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.WebApi.Utility.Filter
{

    public class CustomExceptionFilterAttribute : Attribute, IExceptionFilter
    {

        private ILogger<CustomActionFilterAttribute> _Logger = null;

        public CustomExceptionFilterAttribute(ILogger<CustomActionFilterAttribute> logger)
        {
            _Logger = logger;
        }

        /// <summary>
        /// 发生异常之后触发到这儿
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {

            try
            {
                //在这里是不允许发生异常
                _Logger.LogError(context.Exception.Message);
                //处理异常 
                {

                    throw new Exception("ExceptionFilter  内部发生异常~~");

                    //发个邮件
                    //发个信息
                }
                context.ExceptionHandled = true; //标记当前抛出的异常已经被处理过了
            }
            catch (Exception)
            {

                throw;
            }

           
          
        }
    } 
}
