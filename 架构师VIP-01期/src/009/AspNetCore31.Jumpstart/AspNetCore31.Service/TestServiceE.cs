using AspNetCore31.Interface;
using System;

namespace AspNetCore31.Service
{
    public class TestServiceE : ITestServiceE
    {
        public TestServiceE(ITestServiceC serviceC)
        {
            Console.WriteLine($"{this.GetType().Name}被构造。。。");
        }

        public void Show()
        {
            Console.WriteLine("E123456");
        }
    }
}
