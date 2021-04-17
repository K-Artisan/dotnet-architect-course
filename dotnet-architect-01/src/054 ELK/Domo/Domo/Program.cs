using System;

namespace Domo
{
    class Program
    {
        static void Main(string[] args)
        {
            //-----------一个业务请求流程：订单
            //生成唯一一个请求Id
            AllLinkModel.RequestID = Guid.NewGuid().ToString();

            new MysqlHelper().Do();
            new RedisHelper().Do();
            new KafkaHelper().Do();

        }
    }
}
