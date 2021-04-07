using AspNetCore.RabbitMQ.MessageConsumer_03.ExchangeDemo;
using System;

namespace AspNetCore.RabbitMQ.MessageConsumer_03
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
               
                #region 架构师VIP班-2 
                {
                    //FanoutExchange.Show();
                }
                #endregion

                {
                    TopicExchange.Show();
                }

                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
