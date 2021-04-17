using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog.Fluent;
using Zhaoxi.ELK.Logstach.Models;
using Zhaoxi.ELK.Logstach_File;

namespace Zhaoxi.ELK.Logstach.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;

			logger.LogDebug(".LogDebug()");
			logger.LogTrace("LogTrace");
			RootData rootData = new RootData()
			{
				Code = Guid.NewGuid().ToString(),
				Message = "这是你们的错误  我没有问题哦"
			};
			logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(rootData));
		}

		public IActionResult Index()
		{
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
