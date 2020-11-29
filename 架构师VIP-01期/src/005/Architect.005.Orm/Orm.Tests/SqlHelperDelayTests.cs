using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orm.DAL;
using Orm.Model;
using System;

namespace Orm.Tests
{
    [TestClass]
    public class SqlHelperDelayTests
    {
        [TestMethod]
        public void TestSaveChange_Transations_Exception()
        {
            try
            {
                using (SqlHelperDelay sqlHelperDelay = new SqlHelperDelay())
                {
                    var company1 = sqlHelperDelay.Find<CompanyModel>(1);
                    var company2 = sqlHelperDelay.Find<CompanyModel>(2);

                    //字符串超过数据库字段长度，抛出异常
                    company2.CompanyName += "  var company2 = sqlHelperDelay.Find<CompanyModel>(2);" +
                        "  var company2 = sqlHelperDelay.Find<CompanyModel>(2);" +
                        "  var company2 = sqlHelperDelay.Find<CompanyModel>(2);" +
                        "  var company2 = sqlHelperDelay.Find<CompanyModel>(2);" +
                        "  var company2 = sqlHelperDelay.Find<CompanyModel>(2);" +
                        "  var company2 = sqlHelperDelay.Find<CompanyModel>(2);" +
                        "  var company2 = sqlHelperDelay.Find<CompanyModel>(2);" +
                        "  var company2 = sqlHelperDelay.Find<CompanyModel>(2);" +
                        "  var company2 = sqlHelperDelay.Find<CompanyModel>(2);";

                    sqlHelperDelay.Insert(company1);
                    sqlHelperDelay.Insert(company2);
                    sqlHelperDelay.SaveChange();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        [TestMethod]
        public void TestSaveChange_Transations_Ok()
        {
            try
            {
                using (SqlHelperDelay sqlHelperDelay = new SqlHelperDelay())
                {
                    var company1 = sqlHelperDelay.Find<CompanyModel>(1);
                    company1.CreateTime = DateTime.Now;
                    var company2 = sqlHelperDelay.Find<CompanyModel>(2);
                    company2.CompanyName += "Transations_Ok";
                    company2.CreateTime = DateTime.Now;

                    sqlHelperDelay.Insert(company1);
                    sqlHelperDelay.Insert(company2);
                    sqlHelperDelay.SaveChange();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
