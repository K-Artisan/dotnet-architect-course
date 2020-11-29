using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore31.Jumpstart.Utiltiy
{
    public class CustomActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"This {nameof(CustomActionFilterAttribute)} OnActionExecuted->Order:{this.Order}");
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"This {nameof(CustomActionFilterAttribute)} OnActionExecuting->Order:{this.Order}");
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine($"This {nameof(CustomActionFilterAttribute)} OnResultExecuting->Order:{this.Order}");
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine($"This {nameof(CustomActionFilterAttribute)} OnResultExecuted->Order:{this.Order}");
        }
    }

    public class CustomControllerFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"This {nameof(CustomControllerFilterAttribute)} OnActionExecuted ->Order:{this.Order}");
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"This {nameof(CustomControllerFilterAttribute)} OnActionExecuting->Order:{this.Order}");
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine($"This {nameof(CustomControllerFilterAttribute)} OnResultExecuting->Order:{this.Order}");
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine($"This {nameof(CustomControllerFilterAttribute)} OnResultExecuted->Order:{this.Order}");
        }
    }

    public class CustomGlobalFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"This {nameof(CustomGlobalFilterAttribute)} OnActionExecuted->Order:{this.Order}");
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"This {nameof(CustomGlobalFilterAttribute)} OnActionExecuting->Order:{this.Order}");
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine($"This {nameof(CustomGlobalFilterAttribute)} OnResultExecuting->Order:{this.Order}");
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine($"This {nameof(CustomGlobalFilterAttribute)} OnResultExecuted->Order:{this.Order}");
        }
    }
}
