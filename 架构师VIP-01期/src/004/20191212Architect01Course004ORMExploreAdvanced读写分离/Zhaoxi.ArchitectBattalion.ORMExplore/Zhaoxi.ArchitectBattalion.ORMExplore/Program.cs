using System;
using System.Threading;
using Zhaoxi.ArchitectBattalion.DAL;
using Zhaoxi.ArchitectBattalion.Model;

namespace Zhaoxi.ArchitectBattalion.ORMExplore
{
    /// <summary>
    /// 手写ORM
    /// 
    /// .NetCore和Asp.NetCore绑定紧密  控制台其实没管
    /// 
    /// 继续手写O/RM  搞定读写分离
    /// 
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("欢迎来到.Net架构班，一起来撸码吧~");
                #region 1204
                //SqlHelper helper = new SqlHelper();
                //CompanyModel company1= helper.Find<CompanyModel>(1);
                //CompanyModel company2 = helper.Find<CompanyModel>(2);

                //helper.Insert<CompanyModel>(company1);
                //helper.Insert<CompanyModel>(company2);

                //User user1 = helper.Find<User>(1);
                //User user7 = helper.Find<User>(7);
                //CompanyModel company3 = helper.Find<CompanyModel>(3);
                //CompanyModel company7 = helper.Find<CompanyModel>(7);
                //User user8 = helper.Find<User>(8);
                #endregion

                #region 1212
                {
                    //SqlHelper helper = new SqlHelper();
                    //CompanyModel company1 = helper.Find<CompanyModel>(1);
                    //company1.CreateTime = DateTime.Now;
                    //company1.CompanyName += "-Administrator";
                    //int id = helper.Insert<CompanyModel>(company1);
                    //Console.WriteLine($"新增的CompanyId={id}");
                    //for (int i = 0; i < 100; i++)
                    //{
                    //    CompanyModel companyNew = helper.Find<CompanyModel>(id);
                    //    if (companyNew == null)
                    //    {
                    //        Console.WriteLine($"keep moving {i}");
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine($"第{i}*500ms 完成同步");
                    //        break;
                    //    }
                    //    Thread.Sleep(500);
                    //}
                }
                {
                    //SqlHelper helper = new SqlHelper();
                    //CompanyModel company1 = helper.Find<CompanyModel>(80);
                    //company1.CreateTime = DateTime.Now;
                    //company1.CompanyName += "-Administrator";
                    ////helper.Update<CompanyModel>(company1);

                    ////需要数据验证

                }
                {
                    SqlHelper helper = new SqlHelper();
                    helper.Update<CompanyModel>(Newtonsoft.Json.JsonConvert.SerializeObject(
                        new
                        {
                            CompanyName = "landonzeng",
                            CreateTime = DateTime.Now,
                            LastModifyTime = DateTime.Now
                        }), 81);
                }

                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
