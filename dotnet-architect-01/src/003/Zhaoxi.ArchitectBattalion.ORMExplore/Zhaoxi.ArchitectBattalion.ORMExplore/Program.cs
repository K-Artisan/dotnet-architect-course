using System;
using Zhaoxi.ArchitectBattalion.DAL;
using Zhaoxi.ArchitectBattalion.Model;

namespace Zhaoxi.ArchitectBattalion.ORMExplore
{
    /// <summary>
    /// 手写ORM
    /// 
    /// .NetCore和Asp.NetCore绑定紧密  控制台其实没管
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("欢迎来到.Net架构班，一起来撸码吧~");
                SqlHelper helper = new SqlHelper();
                CompanyModel company1= helper.Find<CompanyModel>(1);
                CompanyModel company2 = helper.Find<CompanyModel>(2);

                helper.Insert<CompanyModel>(company1);
                helper.Insert<CompanyModel>(company2);

                User user1 = helper.Find<User>(1);
                User user7 = helper.Find<User>(7);
                CompanyModel company3 = helper.Find<CompanyModel>(3);
                CompanyModel company7 = helper.Find<CompanyModel>(7);
                User user8 = helper.Find<User>(8);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
