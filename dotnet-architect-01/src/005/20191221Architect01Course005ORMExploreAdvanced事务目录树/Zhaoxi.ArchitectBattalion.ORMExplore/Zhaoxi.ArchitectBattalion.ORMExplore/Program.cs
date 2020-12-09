using System;
using System.Threading;
using Zhaoxi.ArchitectBattalion.DAL;
using Zhaoxi.ArchitectBattalion.Framework;
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
    /// 今晚有很多内容，还有很多通知！
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

                #region 1221
                //{
                //    SqlHelper helper = new SqlHelper();
                //    CompanyModel company1 = helper.Find<CompanyModel>(1);
                //    CompanyModel company2 = helper.Find<CompanyModel>(2);
                //    company1.CreateTime = DateTime.Now;
                //    company2.CreateTime = DateTime.Now;

                //    //希望两个Sql一起成功  一起失败  数据库事务
                //    //helper.Insert<CompanyModel>(company1);
                //    company2.CompanyName += "123";// "helper.Inser(company1)helper.Insert<CompanyModel>(company1)helper.Insert<CompanyModel>(company1)helper.Insert<CompanyModel>(company1)helper.Insert<CompanyModel>(company1)helper.Insert<CompanyModel>(company1)helper.Insert<CompanyModel>(company1)helper.Insert<CompanyModel>(company1)helper.Insert<CompanyModel>(company1)helper.Insert<CompanyModel>(company1)helper.Insert<CompanyModel>(company1)helper.Insert<CompanyModel>(company1)helper.Insert<CompanyModel>(company1)helper.Insert<CompanyModel>(company1)helper.Insert<CompanyModel>(company1)helper.Insert<CompanyModel>(company1)helper.Insert<CompanyModel>(company1)helper.Insert<CompanyModel>(company1)helper.Insert<CompanyModel>(company1)helper.Insert<CompanyModel>(company1)helper.Insert<CompanyModel>(company1)helper.Insert<CompanyModel>(company1)";

                //    //helper.Insert<CompanyModel>(company2);

                //    //能不能多次操作，用一个conn字符串----希望做成像DbContext---多个操作命令---一次性延迟提交
                //    //{
                //    //    using (SqlHelperDelay sqlHelperDelay = new SqlHelperDelay())
                //    //    {
                //    //        sqlHelperDelay.Insert<CompanyModel>(company1);
                //    //        sqlHelperDelay.Insert<CompanyModel>(company2);
                //    //        sqlHelperDelay.SaveChange();
                //    //    }
                //    //}

                //    //UnitOfWork--多个链接保证事务---TransactionScope---单数据库多链接对象的事务
                //    //using (IUnitOfWork unitOfWork = new UnitOfWork())
                //    //{
                //    //    unitOfWork.Invoke(() =>
                //    //    {
                //    //        helper.Insert<CompanyModel>(company1);
                //    //        helper.Insert<CompanyModel>(company2);
                //    //    });
                //    //}

                //    //不同的数据库当然是不同的链接--就是分布式事务---需要启动下电脑DTC服务---.NetCore2.1之后就不支持了---不过，我这会儿去framework给你们验证下---
                //    //在.NetFramework下面，MSDTC确实可以保证多个数据库中间完成事务，
                //    //using (IUnitOfWork unitOfWork = new UnitOfWork())
                //    //{
                //    //    unitOfWork.Invoke(() =>
                //    //    {
                //    //        helper.Insert<CompanyModel>(company1);//写库
                //    //        new SqlHelperCopy().Insert<CompanyModel>(company2);//copy
                //    //    });
                //    //}

                //}
                {
                    SqlHelper helper = new SqlHelper();
                    //var companys = helper.FindCondition<CompanyModel>(c => c.Id > 10
                    //&& c.CompanyName.StartsWith("朝夕")
                    //&& c.CompanyName.EndsWith("教育")
                    //&& c.CompanyName.Contains("朝夕"));

                    //int id = 10;
                    //var companys = helper.FindCondition<CompanyModel>(c => c.Id > id
                    // && c.Id > 20
                    //|| c.CompanyName.StartsWith("朝夕")
                    //&& c.CompanyName.EndsWith("教育")
                    //&& c.CompanyName.Contains("朝夕"));

                    ////参数化
                    //int id = 10;
                    //var companys = helper.FindCondition<CompanyModel>(c => c.Id > id
                    //             && c.CompanyName.StartsWith("朝夕")
                    //             && c.CompanyName.EndsWith("教育")
                    //             && c.CompanyName.Contains("朝夕"));
                    //是不是可以批量查询----批量删除----批量更新？
                    //helper.Update<T>(c => c.Id > id, t => t.State == 1)//如果有好办法

                    //分页--自己试试呗，row_number

                    //InnerJoin---只管2个表的，t.Id,s.CompanyId
                    //22: 25 开始提问
                    //22：30开始答疑
                    //期间老师不说话-无声模式，问题建议不要刷屏！

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
