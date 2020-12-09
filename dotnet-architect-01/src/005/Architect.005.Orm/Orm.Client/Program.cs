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
                SqlHelper helper = new SqlHelper();
                var companys = helper.FindCondition<CompanyModel>(c => c.Id > 3 && c.CompanyName.StartsWith("k"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
