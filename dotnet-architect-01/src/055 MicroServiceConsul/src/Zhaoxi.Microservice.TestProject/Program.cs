using System;

namespace Zhaoxi.Microservice.TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");
                {
                    JsonVSProtobuf.Show();
                }
                {
                    //DistributedLockTest.Show();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
