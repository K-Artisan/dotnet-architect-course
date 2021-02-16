﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Zhaoxi.AspNetCore3_1.Interface;
using Zhaoxi.EntityFrameworkCore31.Model;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.AspNetCore.Authentication;

namespace Zhaoxi.AspNetCore31.Demo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class UsersNewController : ControllerBase
    {
        #region MyRegion
        private ILoggerFactory _Factory = null;
        private ILogger<UsersNewController> _logger = null;
        private ITestServiceA _ITestServiceA = null;
        private ITestServiceB _ITestServiceB = null;
        private ITestServiceC _ITestServiceC = null;
        private ITestServiceD _ITestServiceD = null;
        private IUserService _IUserService = null;
        private List<Users> _userList = null;
        private readonly IConfiguration _iConfiguration;
        private readonly IUserService _iUserService;
        public ITestServiceA ITestServiceA { get; set; }
        public UsersNewController(ILoggerFactory factory,
            ILogger<UsersNewController> logger,
            ITestServiceA testServiceA,
            ITestServiceB testServiceB,
            ITestServiceC testServiceC,
            ITestServiceD testServiceD,
            IConfiguration configuration
            , IUserService userService)
        {
            this._Factory = factory;
            this._logger = logger;
            this._ITestServiceA = testServiceA;
            this._ITestServiceB = testServiceB;
            this._ITestServiceC = testServiceC;
            this._ITestServiceD = testServiceD;
            this._IUserService = userService;
            this._iConfiguration = configuration;
            this._iUserService = userService;


            #region 为了测试方便，不从数据获取数据，使用内存
            //this._userList = this._iUserService.Query<Zhaoxi.EntityFrameworkCore31.Model.User>(u => u.Id > 1)
            //                    .OrderBy(u => u.Id)
            //                    .Skip(1)
            //                    .Take(5)
            //                    .Select(u => new Users()
            //                    {
            //                        UserID = u.Id,
            //                        UserEmail = u.Email,
            //                        UserName = u.Name
            //                    }).ToList();

            this._userList = new List<Users>() {
                    new Users{ UserID = 1, UserName = "User-01"},
                    new Users{ UserID = 2, UserName = "User-02"},
                    new Users{ UserID = 3, UserName = "User-03"},
                    new Users{ UserID = 4, UserName = "User-04"},
                    new Users{ UserID = 5, UserName = "User-05"},
                    new Users{ UserID = 6, UserName = "User-06"},
            };

            #endregion
        }
        #endregion

        #region HttpGet
        // GET api/Users
        [HttpGet]
        public IEnumerable<Users> Get()
        {
            return _userList;
        }

        [HttpGet]
        public IEnumerable<Users> GetExcetion()
        {
            this._logger.LogWarning($"{DateTime.Now.ToString("HH:mm:ss fff")} {this.GetType()} GetExcetion......");
            throw new Exception();
        }

        // GET api/Users/5
        [HttpGet]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public Users GetUserByID(int id)
        {
            base.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");//允许跨域
            //throw new Exception("1234567");
            string idParam = base.HttpContext.Request.Query["id"];
            var user = _userList.FirstOrDefault(users => users.UserID == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;

        }

        //GET api/Users/?username=xx
        [HttpGet]
        //[CustomBasicAuthorizeAttribute]
        //[CustomExceptionFilterAttribute]
        //[RouteAttribute("GetUserByName")]
        public IEnumerable<Users> GetUserByName(string userName)
        {
            //throw new Exception("1234567");
            var nickName = HttpContext.AuthenticateAsync().Result.Principal.Claims.FirstOrDefault(c => c.Type.Equals("NickName"))?.Value;
            Console.WriteLine($"This is GetUserByName 校验 {nickName}");

            var role = HttpContext.AuthenticateAsync().Result.Principal.Claims.FirstOrDefault(c => c.Type.Equals("role"))?.Value;
            Console.WriteLine($"This is role 校验 {role}");

            string userNameParam = base.HttpContext.Request.Query["userName"];

            return _userList.Where(p => string.Equals(p.UserName, userName, StringComparison.OrdinalIgnoreCase));
        }

        //GET api/Users/?username=xx&id=1
        [HttpGet]
        public IEnumerable<Users> GetUserByNameId(string userName, int id)
        {
            string idParam = base.HttpContext.Request.Query["id"];
            string userNameParam = base.HttpContext.Request.Query["userName"];

            return _userList.Where(p => string.Equals(p.UserName, userName, StringComparison.OrdinalIgnoreCase));
        }

        [HttpGet]
        public IEnumerable<Users> GetUserByModel([FromUri]Users user)
        {
            string idParam = base.HttpContext.Request.Query["id"];
            string userNameParam = base.HttpContext.Request.Query["userName"];
            string emai = base.HttpContext.Request.Query["email"];

            return _userList;
        }

        [HttpGet]
        public IEnumerable<Users> GetUserByModelUri([FromUri]Users user)
        {
            string idParam = base.HttpContext.Request.Query["id"];
            string userNameParam = base.HttpContext.Request.Query["userName"];
            string emai = base.HttpContext.Request.Query["email"];

            return _userList;
        }

        [HttpGet]
        public IEnumerable<Users> GetUserByModelSerialize(string userString)
        {
            Users user = JsonConvert.DeserializeObject<Users>(userString);
            return _userList;
        }

        //[HttpGet]
        public IEnumerable<Users> GetUserByModelSerializeWithoutGet(string userString)
        {
            Users user = JsonConvert.DeserializeObject<Users>(userString);
            return _userList;
        }
        /// <summary>
        /// 方法名以Get开头，WebApi会自动默认这个请求就是get请求，而如果你以其他名称开头而又不标注方法的请求方式，那么这个时候服务器虽然找到了这个方法，但是由于请求方式不确定，所以直接返回给你405——方法不被允许的错误。
        /// 最后结论：所有的WebApi方法最好是加上请求的方式（[HttpGet]/[HttpPost]/[HttpPut]/[HttpDelete]），不要偷懒，这样既能防止类似的错误，也有利于方法的维护，别人一看就知道这个方法是什么请求。
        /// </summary>
        /// <param name="userString"></param>
        /// <returns></returns>
        public IEnumerable<Users> NoGetUserByModelSerializeWithoutGet(string userString)
        {
            Users user = JsonConvert.DeserializeObject<Users>(userString);
            return _userList;
        }
        #endregion HttpGet

        #region HttpPost
        //POST api/Users/RegisterNone
        [HttpPost]
        public Users RegisterNone()
        {
            return _userList.FirstOrDefault();
        }

        [HttpPost]
        public Users RegisterNoKey([FromBody]int id)
        {
            string idParam = base.HttpContext.Request.Form["id"];

            var user = _userList.FirstOrDefault(users => users.UserID == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        //POST api/Users/register
        //只接受一个参数的需要不给key才能拿到
        [HttpPost]
        public Users Register([FromBody]int id)//可以来自FromBody   FromUri
                                               //public Users Register(int id)//可以来自url
        {
            string idParam = base.HttpContext.Request.Form["id"];

            var user = _userList.FirstOrDefault(users => users.UserID == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        //POST api/Users/RegisterUser
        [HttpPost]
        public Users RegisterUser(Users user)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["UserID"];
            string nameParam = base.HttpContext.Request.Form["UserName"];
            string emailParam = base.HttpContext.Request.Form["UserEmail"];

            return user;
        }


        //POST api/Users/register
        [HttpPost]
        public string RegisterObject(JObject jData)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["User[UserID]"];
            string nameParam = base.HttpContext.Request.Form["User[UserName]"];
            string emailParam = base.HttpContext.Request.Form["User[UserEmail]"];
            string infoParam = base.HttpContext.Request.Form["info"];
            dynamic json = jData;
            JObject jUser = json.User;
            string info = json.Info;
            var user = jUser.ToObject<Users>();

            return string.Format("{0}_{1}_{2}_{3}", user.UserID, user.UserName, user.UserEmail, info);
        }

        [HttpPost]
        public string RegisterObjectDynamic(dynamic dynamicData)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["User[UserID]"];
            string nameParam = base.HttpContext.Request.Form["User[UserName]"];
            string emailParam = base.HttpContext.Request.Form["User[UserEmail]"];
            string infoParam = base.HttpContext.Request.Form["info"];
            dynamic json = dynamicData;
            JObject jUser = json.User;
            string info = json.Info;
            var user = jUser.ToObject<Users>();

            return string.Format("{0}_{1}_{2}_{3}", user.UserID, user.UserName, user.UserEmail, info);
        }
        #endregion HttpPost

        #region HttpPut
        //POST api/Users/RegisterNonePut
        [HttpPut]
        public Users RegisterNonePut()
        {
            return _userList.FirstOrDefault();
        }

        [HttpPut]
        public Users RegisterNoKeyPut([FromBody]int id)
        {
            string idParam = base.HttpContext.Request.Form["id"];

            var user = _userList.FirstOrDefault(users => users.UserID == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        //POST api/Users/registerPut
        //只接受一个参数的需要不给key才能拿到
        [HttpPut]
        public Users RegisterPut([FromBody]int id)//可以来自FromBody   FromUri
                                                  //public Users Register(int id)//可以来自url
        {
            string idParam = base.HttpContext.Request.Form["id"];

            var user = _userList.FirstOrDefault(users => users.UserID == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        //POST api/Users/RegisterUserPut
        [HttpPut]
        public Users RegisterUserPut(Users user)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["UserID"];
            string nameParam = base.HttpContext.Request.Form["UserName"];
            string emailParam = base.HttpContext.Request.Form["UserEmail"];

            return user;
        }


        //POST api/Users/registerPut
        [HttpPut]
        public string RegisterObjectPut(JObject jData)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["User[UserID]"];
            string nameParam = base.HttpContext.Request.Form["User[UserName]"];
            string emailParam = base.HttpContext.Request.Form["User[UserEmail]"];
            string infoParam = base.HttpContext.Request.Form["info"];
            dynamic json = jData;
            JObject jUser = json.User;
            string info = json.Info;
            var user = jUser.ToObject<Users>();

            return string.Format("{0}_{1}_{2}_{3}", user.UserID, user.UserName, user.UserEmail, info);
        }

        [HttpPut]
        public string RegisterObjectDynamicPut(dynamic dynamicData)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["User[UserID]"];
            string nameParam = base.HttpContext.Request.Form["User[UserName]"];
            string emailParam = base.HttpContext.Request.Form["User[UserEmail]"];
            string infoParam = base.HttpContext.Request.Form["info"];
            dynamic json = dynamicData;
            JObject jUser = json.User;
            string info = json.Info;
            var user = jUser.ToObject<Users>();

            return string.Format("{0}_{1}_{2}_{3}", user.UserID, user.UserName, user.UserEmail, info);
        }
        #endregion HttpPut

        #region HttpDelete
        //POST api/Users/RegisterNoneDelete
        [HttpDelete]
        public Users RegisterNoneDelete()
        {
            return _userList.FirstOrDefault();
        }

        [HttpDelete]
        public Users RegisterNoKeyDelete([FromBody]int id)
        {
            string idParam = base.HttpContext.Request.Form["id"];

            var user = _userList.FirstOrDefault(users => users.UserID == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        //POST api/Users/registerDelete
        //只接受一个参数的需要不给key才能拿到
        [HttpDelete]
        public Users RegisterDelete([FromBody]int id)//可以来自FromBody   FromUri
                                                     //public Users Register(int id)//可以来自url
        {
            string idParam = base.HttpContext.Request.Form["id"];

            var user = _userList.FirstOrDefault(users => users.UserID == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        //POST api/Users/RegisterUserDelete
        [HttpDelete]
        public Users RegisterUserDelete(Users user)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["UserID"];
            string nameParam = base.HttpContext.Request.Form["UserName"];
            string emailParam = base.HttpContext.Request.Form["UserEmail"];
            return user;
        }


        //POST api/Users/registerDelete
        [HttpDelete]
        public string RegisterObjectDelete(JObject jData)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["User[UserID]"];
            string nameParam = base.HttpContext.Request.Form["User[UserName]"];
            string emailParam = base.HttpContext.Request.Form["User[UserEmail]"];
            string infoParam = base.HttpContext.Request.Form["info"];
            dynamic json = jData;
            JObject jUser = json.User;
            string info = json.Info;
            var user = jUser.ToObject<Users>();

            return string.Format("{0}_{1}_{2}_{3}", user.UserID, user.UserName, user.UserEmail, info);
        }

        [HttpDelete]
        public string RegisterObjectDynamicDelete(dynamic dynamicData)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["User[UserID]"];
            string nameParam = base.HttpContext.Request.Form["User[UserName]"];
            string emailParam = base.HttpContext.Request.Form["User[UserEmail]"];
            string infoParam = base.HttpContext.Request.Form["info"];
            dynamic json = dynamicData;
            JObject jUser = json.User;
            string info = json.Info;
            var user = jUser.ToObject<Users>();

            return string.Format("{0}_{1}_{2}_{3}", user.UserID, user.UserName, user.UserEmail, info);
        }
        #endregion HttpDelete
    }
}