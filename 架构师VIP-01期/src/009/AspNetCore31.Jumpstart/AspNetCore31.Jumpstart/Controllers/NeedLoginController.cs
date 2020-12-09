using AspNetCore31.Jumpstart.Utiltiy;
using AspNetCore31.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore31.Jumpstart.Controllers
{
    //[TypeFilter(typeof(CustomCheckLoginActionFilter))]
    [Authorize]
    public class NeedLoginController : Controller
    {
        private readonly JDDbContext _dbContext ;
        public NeedLoginController(JDDbContext jdDbContext)
        {
            _dbContext = jdDbContext;
        }

        public IActionResult Index()
        {

            var currUser = HttpContext.User.Identity.Name;


            //using (JDDbContext context = new JDDbContext())
            //{
            //    var user = context.Set<User>().First(u => u.Id > 1);
            //    base.ViewBag.UserName = user.Name;
            //}
           
           // var user = _dbContext.Set<User>().First(u => u.Id > 1);

            return View();
        }
    }
}
