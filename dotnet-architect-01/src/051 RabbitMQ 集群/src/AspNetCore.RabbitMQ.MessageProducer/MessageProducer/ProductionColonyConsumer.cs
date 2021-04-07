using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AspNetCore.RabbitMQ.MessageProducer.MessageProducer
{
    public class ProductionColonyConsumer
    {
        public static void Show()
        {
            ConnectionFactory factory = new ConnectionFactory();
            //factory.HostName = "192.168.130.129";//RabbitMQ服务在本地运行 

            factory.Port = 5672;
            factory.UserName = "rabbit";//用户名
            factory.Password = "rabbit";//密码 
            //factory.AutomaticRecoveryEnabled = true; //如果connection挂掉是否重新连接 
            //factory.TopologyRecoveryEnabled = true;//连接恢复后，连接的交换机，队列等是否一同恢复 
            factory.VirtualHost = "/";  //虚拟主机，默认斜杠
            using (var connection = factory.CreateConnection(new List<string>() {
                    "192.168.130.129",
                    "192.168.130.130",
                    "192.168.130.131"
            }))
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "ColonyProducerMessage", durable: true, exclusive: false, autoDelete: false, arguments: null);

                    channel.ExchangeDeclare(exchange: "ColonyProducerMessageExChange", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);

                    channel.QueueBind(queue: "ColonyProducerMessage", exchange: "ColonyProducerMessageExChange", routingKey: "ColonyProducerMessageExChangeKey", arguments: null);

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("生产者ColonyProducerDemo已准备就绪~~~");
                    int i = 1;
                    {
                        while (true)
                        {
                            IBasicProperties basicProperties = channel.CreateBasicProperties();
                            basicProperties.Persistent = true;
                            string message = $"消息{i}";
                            byte[] body = Encoding.UTF8.GetBytes(message);
                            channel.BasicPublish(exchange: "ColonyProducerMessageExChange",
                                            routingKey: "ColonyProducerMessageExChangeKey",
                                            basicProperties: basicProperties,
                                            body: body);
                            Console.WriteLine($"集群消息：{message} 已发送~");
                            i++;
                            Thread.Sleep(10);

                            //if (i >= 200)
                            //{
                            //    break;
                            //}
                                    
                        }
                    }
                }
            }
        }
    }
}
