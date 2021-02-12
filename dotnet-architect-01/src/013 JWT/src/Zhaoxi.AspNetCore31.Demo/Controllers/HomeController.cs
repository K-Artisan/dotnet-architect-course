using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Zhaoxi.AspNetCore31.Demo.Models;
using Zhaoxi.EntityFrameworkCore31.Model;

namespace Zhaoxi.AspNetCore31.Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbContext _dbContext;
        public HomeController(ILogger<HomeController> logger
            , JDDbContext dbContext, DbContext dbContext1)
        {
            _logger = logger;
            this._dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var user = this._dbContext.Set<User>().First(u => u.Id > 1);
            base.ViewBag.UserName = user.Name;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
