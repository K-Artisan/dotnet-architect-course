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

        public FirstController(ILogger<FirstController> logger, ILoggerFactory loggerFactory)
        {
            _logger = logger;
            _loggerFactory = loggerFactory;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("log in FirstController");
            _loggerFactory.CreateLogger<FirstController>().LogInformation("log in FirstController");

            return View();
        }
    }
}
