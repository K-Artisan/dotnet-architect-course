using AspNetCore31.Jumpstart.Models;
using AspNetCore31.Jumpstart.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore31.Jumpstart.Utiltiy
{
    public class CustomCheckLoginActionFilter : ActionFilterAttribute
    {
        #region Identity
        private readonly ILogger<CustomCheckLoginActionFilter> _logger;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        public CustomCheckLoginActionFilter(Microsoft.Extensions.Logging.ILogger<CustomCheckLoginActionFilter> logger
            )
        {
            this._logger = logger;
        }
        #endregion

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //从Session中获取
            CurrentUser currentUser = context.HttpContext.GetCurrentUserBySession();
            //var currUser = context.HttpContext.User.Identity.Name;
            if (currentUser == null)
            {
                //if (this.IsAjaxRequest(context.HttpContext.Request))
                //{ }
                context.Result = new RedirectResult("~/Account/Login");
            }
            else
            {
                this._logger.LogDebug($"{currentUser.Name} 访问系统");
            }
        }
        private bool IsAjaxRequest(HttpRequest request)
        {
            string header = request.Headers["X-Requested-With"];
            return "XMLHttpRequest".Equals(header);
        }
    }
}
