using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AspNetCore.RabbitMQ.MessageConsumer_01.Advanced
{
    public class ConsumptionColonyACKConfirm
    {
        public static void Show()
        {
            var factory = new ConnectionFactory();
            //factory.HostName = "192.168.130.129";//RabbitMQ服务在本地运行
            factory.Port = 5672;
            factory.UserName = "rabbit";//用户名
            factory.Password = "rabbit";//密码 
            factory.VirtualHost = "/";  //虚拟主机，默认斜杠
            using (var connection = factory.CreateConnection(new List<string>() {
                    "192.168.130.129",
                    "192.168.130.130",
                    "192.168.130.131"
            }))
            {
                using (IModel channel = connection.CreateModel())
                {
                    #region EventingBasicConsumer
                    //定义消费者                                      
                    var consumer = new EventingBasicConsumer(channel);
                    int i = 0;
                    consumer.Received += (model, ea) =>
                    {
                        var message = Encoding.UTF8.GetString(ea.Body.ToArray()); 
                        //手动确认  消息正常消费  告诉Broker：你可以把当前这条消息删除掉了
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                        Console.WriteLine(message);
                        Thread.Sleep(2000);
                    };
                    //autoAck: false  显示确认； 
                    channel.BasicConsume(queue: "ColonyProducerMessage", autoAck: false, consumer: consumer); 
                    Console.ReadKey();
                    #endregion
                }
            }
        }
    }
}
