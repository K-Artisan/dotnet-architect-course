using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.RabbitMQ.MessageProducer.Advanced
{
    public class PriorityQueue
    {
        public static void Show()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";//RabbitMQ服务在本地运行
            factory.UserName = "guest";//用户名
            factory.Password = "guest";//密码 
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    //指定队列要支持优先级设置，其最后优先级为10；
                    channel.QueueDeclare(queue: "PriorityQueue", durable: true, exclusive: false, autoDelete: false, 
                        arguments: new Dictionary<string, object>() {
                         {"x-max-priority",10 } 
                       });

                    //声明交换机exchang
                    channel.ExchangeDeclare(exchange: "PriorityQueueExchange", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
                    //绑定exchange和queue
                    channel.QueueBind(queue: "PriorityQueue", exchange: "PriorityQueueExchange", routingKey: "PriorityKey");
                   
                    //一些待发送的消息
                    {
                        IBasicProperties props = channel.CreateBasicProperties();
                        props.DeliveryMode = 2; //消息持久化
                        int i = 1;
                        while (true)
                        //当发送的消息很少（比如：5）时，设置优先级无意义 。
                        //动态的生成消息中去插队,故使用while循环，而不能使用for(因为预先就设置好了队列顺序)
                        //for (int i = 1; i <= 100; i++)
                        {
                            props.Priority = 1;
                            string strMessage = $"消息：{i}";
                            channel.BasicPublish(exchange: "PriorityQueueExchange",
                                           routingKey: "PriorityKey",
                                           basicProperties: props,
                                           body: Encoding.UTF8.GetBytes(strMessage));
                            Console.WriteLine($"{strMessage} 已发送。。。。");
                            i++;
                        }

                    }
                    {
                        //string[] questionList = { "vip学员1 来请教", "甲 同学来请教问题", "乙 同学来请教问题", "丙 同学来请教问题", "丁 同学来请教问题", "vip学员2 来请教" };
                        ////设置消息优先级
                        //IBasicProperties props = channel.CreateBasicProperties();
                        //foreach (string questionMsg in questionList)
                        //{
                        //    //有优先级吗？ 
                        //    //channel.BasicPublish(exchange: "PriorityQueueExchange",
                        //    //                  routingKey: "PriorityKey",
                        //    //                  basicProperties: null,
                        //    //                  body: Encoding.UTF8.GetBytes(questionMsg));

                        //    if (questionMsg.StartsWith("vip"))
                        //    {
                        //        props.Priority = 9;
                        //        channel.BasicPublish(exchange: "PriorityQueueExchange",
                        //                       routingKey: "PriorityKey",
                        //                       basicProperties: props,
                        //                       body: Encoding.UTF8.GetBytes(questionMsg));
                        //    }
                        //    else
                        //    {
                        //        props.Priority = 1;
                        //        channel.BasicPublish(exchange: "PriorityQueueExchange",
                        //                       routingKey: "PriorityKey",
                        //                       basicProperties: props,
                        //                       body: Encoding.UTF8.GetBytes(questionMsg));
                        //    }
                        //    Console.WriteLine($"{questionMsg} 已发送~~");
                        //}
                    }
                    Console.Read();
                }
            }
        }
    }
}

