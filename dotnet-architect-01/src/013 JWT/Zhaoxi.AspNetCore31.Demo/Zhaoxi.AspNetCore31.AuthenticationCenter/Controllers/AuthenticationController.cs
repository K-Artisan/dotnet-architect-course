using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Zhaoxi.AspNetCore31.AuthenticationCenter.Utility;

namespace Zhaoxi.AspNetCore31.AuthenticationCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        #region MyRegion
        private ILogger<AuthenticationController> _logger = null;
        private IJWTService _iJWTService = null;
        private readonly IConfiguration _iConfiguration;
        public AuthenticationController(ILoggerFactory factory,
            ILogger<AuthenticationController> logger,
            IConfiguration configuration
            , IJWTService service)
        {
            this._logger = logger;
            this._iConfiguration = configuration;
            this._iJWTService = service;
        }
        #endregion
        [Route("Get")]
        [HttpGet]
        public IEnumerable<int> Get()
        {
            return new List<int>() { 1, 2, 3, 4, 6, 7 };
        }

        [Route("Login")]
        [HttpPost]
        public string Login(string name, string password)
        {
            if ("Eleven".Equals(name) && "123456".Equals(password))//应该数据库
            {
                string token = this._iJWTService.GetToken(name);
                return JsonConvert.SerializeObject(new
                {
                    result = true,
                    token
                });
            }
            else
            {
                return JsonConvert.SerializeObject(new
                {
                    result = false,
                    token = ""
                });
            }
        }
    }
}