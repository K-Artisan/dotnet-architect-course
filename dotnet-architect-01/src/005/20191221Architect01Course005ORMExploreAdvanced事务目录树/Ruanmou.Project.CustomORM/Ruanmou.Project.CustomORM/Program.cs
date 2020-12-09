using Ruanmou.DAL;
using Ruanmou.Libraries.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ruanmou.Project.CustomORM
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("欢迎来到.Net高级班体验课，今天Eleven老师要开启手写ORM专题了");

                SqlHelper helper = new SqlHelper();
                Company company1 = helper.Find<Company>(1);
                Company company2 = helper.Find<Company>(2);
                Company company3 = helper.Find<Company>(3);
                company1.CreateTime = DateTime.Now;
                company2.CreateTime = DateTime.Now;
                company3.CreateTime = DateTime.Now;

                company3.Name += "欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~欢迎来到.Net架构班，一起来撸码吧~";

                using (TransactionScope trans = new TransactionScope())
                {
                    helper.Insert<Company>(company1);
                    helper.Insert<Company>(company2);
                    new SqlHelperCopy().Insert<Company>(company3);
                    trans.Complete();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
//问个问题 插入-更新的时候 不同环境下的  datetime 会有 星期，在数据库会出错，这个 在 反射能解决一下么
