using AspNetCore31.Jumpstart.Utility;
using AspNetCore31.Jumpstart.Models;
using AspNetCore31.Jumpstart.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AspNetCore31.Model;

namespace AspNetCore31.Jumpstart.Controllers
{
    /// <summary>
    /// 登录---常规登录靠的是Cookie/Session
    /// </summary>
    public class AccountController : Controller
    {
        private readonly JDDbContext _dbContext;
        private readonly ILogger<AccountController> _logger;
        public AccountController(ILogger<AccountController> logger, JDDbContext jdDbContext)
        {
            _logger = logger;
            _dbContext = jdDbContext;
        }

        public IActionResult Index()
        {
            try
            {
                var user = _dbContext.Set<User>().First(u => u.Id > 1);
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        [HttpGet]//响应get请求
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        //[CustomAllowAnonymous]
        public async Task<ActionResult> LoginAsync(string name, string password, string verify)
        {
            string verifyCode = base.HttpContext.Session.GetString("CheckCode");
            if (verifyCode != null && verifyCode.Equals(verify, StringComparison.CurrentCultureIgnoreCase))
            {
                if ("KKK".Equals(name) && "123456".Equals(password))
                {
                    CurrentUser currentUser = new CurrentUser()
                    {
                        Id = 123,
                        Name = "KKK",
                        Account = "Administrator",
                        Email = "57265177",
                        Password = "123456",
                        LoginTime = DateTime.Now
                    };
                    #region Cookie/Session 自己写
                    //base.HttpContext.SetCookies("CurrentUser", Newtonsoft.Json.JsonConvert.SerializeObject(currentUser), 30);
                    //base.HttpContext.Session.SetString("CurrentUser", Newtonsoft.Json.JsonConvert.SerializeObject(currentUser));



                    #endregion
                    //过期时间全局设置

                    #region HttpContext.SignInAsync

                    var claims = new List<Claim>()
                                        {
                                            new Claim(ClaimTypes.Name,name),
                                            new Claim("password",password),//可以写入任意数据
                                            new Claim("Account","Administrator")
                                        };
                    var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Customer"));

                    //cookie认证方案:CookieAuthenticationDefaults.AuthenticationScheme
                    //--用户信息:userPrincipal
                    //---过期时间:ExpiresUtc
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                    });//没用await

                    var currUser = HttpContext.User.Identity.Name;
           

                    #endregion

                    return base.Redirect("/Home/Index");
                }
                else
                {
                    base.ViewBag.Msg = "账号密码错误";
                }
            }
            else
            {
                base.ViewBag.Msg = "验证码错误";
            }
            return View();
        }

        public ActionResult VerifyCode()
        {
            string code = "";
            Bitmap bitmap = VerifyCodeHelper.CreateVerifyCode(out code);
            base.HttpContext.Session.SetString("CheckCode", code);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Gif);
            return File(stream.ToArray(), "image/gif");
        }


        [HttpPost]
        //[CustomAllowAnonymous]
        public ActionResult Logout()
        {
            #region Cookie
            base.HttpContext.Response.Cookies.Delete("CurrentUser");
            #endregion Cookie

            #region Session
            CurrentUser sessionUser = base.HttpContext.GetCurrentUserBySession();
            if (sessionUser != null)
            {
                this._logger.LogDebug(string.Format("用户id={0} Name={1}退出系统", sessionUser.Id, sessionUser.Name));
            }
            base.HttpContext.Session.Remove("CurrentUser");
            base.HttpContext.Session.Clear();
            #endregion Session

            #region MyRegion
            //HttpContext.User.Claims//其他信息
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            #endregion
            return RedirectToAction("Index", "Home"); ;
        }
    }
}
