using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orm.DAL;
using Orm.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orm.Tests
{
    [TestClass]
    public class FindConditionTest
    {
        [TestMethod]
        public void TestFindCondition_01()
        {
            SqlHelper helper = new SqlHelper();
            var companys = helper.FindCondition<CompanyModel>(c => c.Id > 10);

            Assert.IsTrue(companys.Count > 0);

            /*            var companys = helper.FindCondition<CompanyModel>(c => c.Id > 10
                        && c.CompanyName.StartsWith("朝夕")
                        && c.CompanyName.EndsWith("教育")
                        && c.CompanyName.Contains("朝夕"));*/

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

        [TestMethod]
        public void TestFindCondition_and()
        {
            SqlHelper helper = new SqlHelper();
            var companys = helper.FindCondition<CompanyModel>(c => c.Id > 3 && c.CompanyName.StartsWith("k"));

            Assert.IsTrue(companys.Count > 0);
        }
    }
}
