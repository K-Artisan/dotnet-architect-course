using Orm.Common.Dto;
using Orm.DAL;
using Orm.Model;
using System;
using System.Threading;

namespace Orm.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                #region 读写分离
                /*             
                               SqlHelper helper = new SqlHelper();
                                CompanyModel company1 = helper.Find<CompanyModel>(1);
                                company1.CreateTime = DateTime.Now;
                                company1.CompanyName += "ss";
                                int id = helper.Insert<CompanyModel>(company1);
                                Console.WriteLine($"新增的CompanyId={id}");
                                for (int i = 0; i < 100; i++)
                                {
                                    Console.WriteLine($"------------------开始查询------------------");
                                    CompanyModel companyNew = helper.Find<CompanyModel>(id);
                                    if (companyNew == null)
                                    {
                                        Console.WriteLine($"未同步到读库，继续{i}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"第{i}*500ms = {i*500/1000}s 同步到从库成功");
                                        break;
                                    }
                                    Console.WriteLine(" ");

                                    Thread.Sleep(500);
                                }
                */
                #endregion

                #region 更新操作:数据验证

                /*                SqlHelper helper = new SqlHelper();
                                CompanyModel company = helper.Find<CompanyModel>(1);
                                company.CompanyName = "";
                                helper.Update<CompanyModel>(company);*/

                #endregion

                #region 更新操作:部分更新

                SqlHelper helper = new SqlHelper();
                string updateJson = Newtonsoft.Json.JsonConvert.SerializeObject(
                        new
                        {
                            CompanyName = "k",
                            LastModifyTime = DateTime.Now
                        }
                    ); ;
     
                helper.Update<CompanyModel>(updateJson, 5);

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
