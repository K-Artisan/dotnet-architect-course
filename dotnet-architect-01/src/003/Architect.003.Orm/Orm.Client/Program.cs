using Orm.Common.Dto;
using Orm.DAL;
using Orm.Model;
using System;

namespace Orm.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SqlHelper helper = new SqlHelper();
                User user = helper.Find<User>(1);
                UserDto userDto = helper.Find<User, UserDto>(1);

                CompanyModel company1 = helper.Find<CompanyModel>(1);
                helper.Insert<CompanyModel>(company1);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
