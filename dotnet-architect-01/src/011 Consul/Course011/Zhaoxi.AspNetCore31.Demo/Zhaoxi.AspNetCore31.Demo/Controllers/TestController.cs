using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Zhaoxi.AspNetCore3_1.Interface;
using Zhaoxi.AspNetCore31.Demo.Utility.WebApiHelper;

namespace Zhaoxi.AspNetCore31.Demo.Controllers
{
    public class TestController : Controller
    {
        #region Identity
        private readonly ILogger<TestController> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ITestServiceA _iTestServiceA;
        private readonly ITestServiceB _iTestServiceB;
        private readonly ITestServiceC _iTestServiceC;
        private readonly ITestServiceD _iTestServiceD;
        private readonly ITestServiceE _iTestServiceE;
        private readonly IServiceProvider _iServiceProvider;
        private readonly IConfiguration _iConfiguration;

        //private readonly DbContext _dbContext;
        private readonly IUserService _iUserService;

        public TestController(ILogger<TestController> logger,
            ILoggerFactory loggerFactory
            , ITestServiceA testServiceA
            , ITestServiceB testServiceB
            , ITestServiceC testServiceC
            , ITestServiceD testServiceD
            , ITestServiceE testServiceE
            , IServiceProvider serviceProvider
            , IConfiguration configuration
            , IUserService userService)
        //, DbContext dbContext)
        {
            this._logger = logger;
            this._loggerFactory = loggerFactory;
            this._iTestServiceA = testServiceA;
            this._iTestServiceB = testServiceB;
            this._iTestServiceC = testServiceC;
            this._iTestServiceD = testServiceD;
            this._iTestServiceE = testServiceE;
            this._iServiceProvider = serviceProvider;
            this._iConfiguration = configuration;
            //this._dbContext = dbContext;
            this._iUserService = userService;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Info()
        {
            List<Users> userList = new List<Users>();
            string resultUrl = null;

            #region 直接调用
            {
                string url = "http://localhost:5726/api/users/get";
                string result = WebApiHelperExtend.InvokeApi(url);
                userList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Users>>(result);
                resultUrl = url;
            }
            #endregion
            #region 这么多地址你怎么管理？1 2 3 要累死--可以自行选择

            #endregion

            #region 通过consul去发现这些服务地址
            {
                using (ConsulClient client = new ConsulClient(c =>
                {
                    c.Address = new Uri("http://localhost:8500/");
                    c.Datacenter = "dc1";
                }))
                {
                    
                    var dictionary = client.Agent.Services().Result.Response;
                    string message = "";
                    foreach (var keyValuePair in dictionary)
                    {
                        AgentService agentService = keyValuePair.Value;
                        this._logger.LogWarning($"{agentService.Address}:{agentService.Port} {agentService.ID} {agentService.Service}");//找的是全部服务 全部实例  其实可以通过ServiceName筛选
                        message += $"{agentService.Address}:{agentService.Port};";
                    }
                    //获取当前consul的全部服务
                    base.ViewBag.Message = message;
                }
            }
            #endregion

            #region 调用---负载均衡
            {
                //string url = "http://localhost:5726/api/users/get";
                //string url = "http://localhost:5727/api/users/get";
                //string url = "http://localhost:5728/api/users/get";
                string url = "http://ZhaoxiUserService/api/users/get";
                //consul解决使用服务名字 转换IP:Port----DNS

                Uri uri = new Uri(url);
                string groupName = uri.Host;

                using (ConsulClient client = new ConsulClient(c =>
                {
                    c.Address = new Uri("http://localhost:8500/");
                    c.Datacenter = "dc1";
                }))
                {
                    var dictionary = client.Agent.Services().Result.Response;
                    var list = dictionary.Where(k => k.Value.Service.Equals(groupName, StringComparison.OrdinalIgnoreCase));
                    KeyValuePair<string, AgentService> keyValuePair = new KeyValuePair<string, AgentService>();

                }
            }
            #endregion

            base.ViewBag.Users = userList;
            base.ViewBag.Url = resultUrl;
            return View();
        }
    }
}