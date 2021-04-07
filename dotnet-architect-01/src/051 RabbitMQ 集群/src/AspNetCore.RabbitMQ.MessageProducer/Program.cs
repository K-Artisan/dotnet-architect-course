using AspNetCore.RabbitMQ.MessageProducer.Advanced;
using AspNetCore.RabbitMQ.MessageProducer.ExchangeDemo;
using AspNetCore.RabbitMQ.MessageProducer.MessageProducer;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AspNetCore.RabbitMQ.MessageProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                #region 架构师VIP班-1 
                {
                    ////生产者消费者
                    // ProductionConsumer.Show();
                }
                {
                    /////多生产者多消费者
                    //IConfigurationRoot config = new ConfigurationBuilder()
                    // .SetBasePath(Directory.GetCurrentDirectory())
                    // .AddCommandLine(args)//支持命令行参数
                    // .Build();
                     
                    //string strMinute = config["minute"];  //什么时候开始执行
                    //string No = config["no"]; //生产者编号 
                    //int minute = Convert.ToInt32(strMinute); 
                    //bool flg = true;
                    //while (flg)
                    //{ 
                    //    if (DateTime.Now.Minute == minute)
                    //    {
                    //        Console.WriteLine($"到{strMinute}分钟，开始写入消息。。。");
                    //        flg = false;
                    //        MultiProductionConsumer.Show(No);
                    //    }
                    //}
                }
                {
                    //互为生产者消费者
                    //Task.Run(() => { MutualProductionConsumer.ShowProductio(); });
                    //Task.Run(() => { MutualProductionConsumer.ShowConsumer(); });
                }
                {
                    ////秒杀
                    //IConfigurationRoot config = new ConfigurationBuilder()
                    // .SetBasePath(Directory.GetCurrentDirectory())
                    // .AddCommandLine(args)//支持命令行参数
                    // .Build();
                    //string strMinute = config["minute"];  //什么时候开始执行 
                    //int minute = Convert.ToInt32(strMinute);

                    //bool flg = true;
                    //while (flg)
                    //{
                    //    Console.WriteLine($"到{strMinute}分钟，开始写入消息。。。");
                    //    if (DateTime.Now.Minute == minute)
                    //    {
                    //        flg = false;
                    //        SeckillConsumer.Show();
                    //    }
                    //}
                }
                {
                    ////优先级 
                    //PriorityQueue.Show(); 
                }
                {
                    ////发布订阅模式
                    //PublishSubscribeConsumer.Show();
                }

                #endregion

                #region 架构师VIP班-2
                {
                   // DirectExchange.Show();
                }
                {
                    //FanoutExchange.Show();
                }
                {
                     //TopicExchange.Show();
                }
                {
                    //HeaderExchange.Show();
                }
                #endregion

                #region 架构师VIP班-3 
                {
                    //ProductionMessageTx.Show();
                } 
                {
                    //ProductionMessageConfirm.Show();
                }
                {
                    //ConsumptionACKConfirm.Show();
                }
                {
                    //集群
                  ProductionColonyConsumer.Show();
                }
                #endregion

                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
