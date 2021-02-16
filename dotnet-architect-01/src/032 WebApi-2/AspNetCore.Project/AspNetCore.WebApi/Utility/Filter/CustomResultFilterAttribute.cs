using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.WebApi.Utility.Filter
{
    public class CustomResultFilterAttribute : IResultFilter
    {
        /// <summary>
        /// ResultFilter 在Api 中用的相对较少
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
