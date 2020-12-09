using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orm.DAL;
using Orm.Framework;
using Orm.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orm.Tests
{
    [TestClass]
    public class UnitOfWorkTests
    {
        [TestMethod]
        public void TestUnitOfWork_Exception()
        {
            using (var uow = new UnitOfWork())
            {
                SqlHelper sqlHelper = new SqlHelper();
                var company1 = sqlHelper.Find<CompanyModel>(1);
                var company2 = sqlHelper.Find<CompanyModel>(2);

                uow.Invoke(() => {
                    sqlHelper.Insert(company1);
                    sqlHelper.Insert(company2);
                    throw new Exception("Exception in Uow!!!");
                });
            }
        }

        [TestMethod]
        public void TestUnitOfWork_Ok()
        {
            using (var uow = new UnitOfWork())
            {
                SqlHelper sqlHelper = new SqlHelper();
                var company1 = sqlHelper.Find<CompanyModel>(1);
                var company2 = sqlHelper.Find<CompanyModel>(2);

                uow.Invoke(() => {
                    sqlHelper.Insert(company1);
                    sqlHelper.Insert(company2);
                });
            }
        }

        [TestMethod]
        public void TestUnitOfWork_MutilDb_Ok()
        {
            using (var uow = new UnitOfWork())
            {
                SqlHelper sqlHelper = new SqlHelper();
                var company1 = sqlHelper.Find<CompanyModel>(1);
                company1.CompanyName += "多数据库事务";
                company1.CreateTime = DateTime.Now;

                SqlHelperOther sqlHelperOther = new SqlHelperOther();
                var company2 = sqlHelperOther.Find<CompanyModel>(1);
                company2.CompanyName += "多数据库事务";
                company1.CreateTime = DateTime.Now;

                uow.Invoke(() => {
                    sqlHelper.Insert(company1);
                    sqlHelperOther.Insert(company2);
                });
            }
        }
    }
}
