using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Zhaoxi.ELK.Logstach_TCP.Models;

namespace Zhaoxi.ELK.Logstach_TCP.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
			logger.LogInformation(1, "{'a':'aa','b':'bb'}");
			logger.LogInformation(1, @"20:13***这是tcp方式的日志啊IActionInvoker
WebApiClient时还没有IActionInvoker概念，对应的执行逻辑直接在ApiActionContext上实现。现在我觉得，Context应该是一个状态数据类，而不能也成为一个执行者，因为一个执行者的实例可以无限次地执行多个Context实例。
Refit则更简单粗暴，将所有实现都在一个RequestBuilderImplementation的类上：你们只要也只能使用我内置的Attribute声明，一切执行在我这个类里面包办，因为我是一个万能类。
Core版本增加了IActionInvoker概念，从中Context分开，用于执行Context，职责分明。在实现上又分为多种Invoker：Task声明返回执行者ActionInvoker、ITask声明返回处理处理者ActionTask，以及聚合的MultiplexedActionInvoker。
Middleware思想WebApiClient时在处理各个特性、参数验证、返回值验证时没有使用Middleware思想，特别是在处理响应结果和异常短路逻辑难以维护。
Refit还是简单粗暴，将所有特性的解释实现都在这个RequestBuilderImplementation的类上，因为我还是一个万能类。
Core版本增加中间件Builder，将请求前的相关Attribute的执行编排Build为一个请求处理委托，将请求后相关Attribute的执行编排Build为一个响应处理委托，然后把两个委托与真实http请求串在一起，Build出一个完整的请求响应委托。
得益于Middleware，流程中的请求前参数值验证、结果处理特性短路、异常短路、请求后结果值验和无条件执行IApiFilterAtrribue等这些复杂的流程变成简单的管道处理；另外接口也变成支持服务端响应多种格式内容，每种格式内容在一个IApiReturnAttribute上实现和处理，比如请求为Accept: application/json, application/xml，不管服务器返回xml或json都能处理");
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
