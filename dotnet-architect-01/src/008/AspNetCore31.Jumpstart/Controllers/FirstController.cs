using AspNetCore31.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore31.Jumpstart.Controllers
{
    public class FirstController : Controller
    {
        private ILogger<FirstController> _logger;
        private ILoggerFactory _loggerFactory;

        private readonly ITestServiceA _iTestServiceA;
        private readonly ITestServiceB _iTestServiceB;
        private readonly ITestServiceC _iTestServiceC;


        public FirstController(ILogger<FirstController> logger, ILoggerFactory loggerFactory, 
            ITestServiceA iTestServiceA,
            ITestServiceB iTestServiceB,
            ITestServiceC iTestServiceC)
        {
            _logger = logger;
            _loggerFactory = loggerFactory;
            _iTestServiceA = iTestServiceA;
            _iTestServiceB = iTestServiceB;
            _iTestServiceC = iTestServiceC;

        }

        public IActionResult Index()
        {
            _logger.LogInformation("log in FirstController");
            _loggerFactory.CreateLogger<FirstController>().LogInformation("log in FirstController");

            Console.WriteLine($"A:{_iTestServiceA.GetHashCode()}");
            Console.WriteLine($"B:{_iTestServiceB.GetHashCode()}");

            Console.WriteLine($"C:{_iTestServiceC.GetHashCode()}");
            Console.WriteLine($"C:{_iTestServiceC.GetHashCode()}");


            return View();
        }
    }
}
