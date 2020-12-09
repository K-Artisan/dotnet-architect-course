using Microsoft.EntityFrameworkCore;
using Zhaoxi.AspNetCore3_1.Interface;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.EntityFrameworkCore31.Model;

namespace Zhaoxi.AspNetCore3_1.Service
{
    public class UserCompanyService : BaseService, IUserCompanyService
    {
        public UserCompanyService(DbContext context) : base(context)
        {
        }

        public void UpdateLastLogin(User user)
        {
            User userDB = base.Find<User>(user.Id);
            userDB.LastLoginTime = DateTime.Now;
            this.Commit();
        }

        public bool RemoveCompanyAndUser(Company company)
        {
            return true;
        }
        //public void Add()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Delete()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Query()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Update()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
