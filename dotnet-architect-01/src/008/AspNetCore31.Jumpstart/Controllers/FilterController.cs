using AspNetCore31.Interface;
using AspNetCore31.Jumpstart.Utiltiy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore31.Jumpstart.Controllers
{
    public class FilterController : Controller
    {
        private readonly ILogger<FilterController> _logger;
        private readonly IConfiguration _configuration;


        private readonly ITestServiceA _iTestServiceA;
        private readonly ITestServiceB _iTestServiceB;
        private readonly ITestServiceC _iTestServiceC;


        public FilterController(IConfiguration configuration,
            ILogger<FilterController> logger,
            ITestServiceA iTestServiceA,
            ITestServiceB iTestServiceB,
            ITestServiceC iTestServiceC)
        {
            _configuration = configuration;
            _logger = logger;
            _iTestServiceA = iTestServiceA;
            _iTestServiceB = iTestServiceB;
            _iTestServiceC = iTestServiceC;

        }


        public IActionResult Index()
        {
            _logger.LogInformation("log in FirstController");

            Console.WriteLine($"A:{_iTestServiceA.GetHashCode()}");
            Console.WriteLine($"B:{_iTestServiceB.GetHashCode()}");

            Console.WriteLine($"C:{_iTestServiceC.GetHashCode()}");
            Console.WriteLine($"C:{_iTestServiceC.GetHashCode()}");


            return View();
        }

        //[CustomExceptionFilterAttribute]//语法错误，无法编译
        //[ServiceFilter(typeof(CustomExceptionFilterAttribute))]
        //[TypeFilter(typeof(CustomExceptionFilterAttribute))]
        [CustomIOCFilterFactory(typeof(CustomExceptionFilterAttribute))]
        public IActionResult Exce()
        {
            throw new Exception("my Exce");
            return View();
        }

        [CustomActionFilterAttribute(Order = -1)]
        public IActionResult ExcOrder()
        {
            Console.WriteLine($"This is {nameof(FilterController)} aciton： ExcOrder ");
            return View();
        }
    }
}
