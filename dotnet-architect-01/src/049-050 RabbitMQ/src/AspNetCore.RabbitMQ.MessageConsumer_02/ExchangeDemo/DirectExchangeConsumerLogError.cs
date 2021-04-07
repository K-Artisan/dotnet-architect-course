using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace AspNetCore.RabbitMQ.MessageConsumer_02.ExchangeDemo
{
    public class DirectExchangeConsumerLogError
    {
        public static void Show()
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";//RabbitMQ服务在本地运行
            factory.UserName = "guest";//用户名
            factory.Password = "guest";//密码 
            using (var connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {

                    #region 可不写，写了只是预防生成者未定义交换机和队列导致不存在而抛异常
                    //channel.QueueDeclare(queue: "DirectExchangeErrorQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    //channel.ExchangeDeclare(exchange: "DirectExChange", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
                    //// 绑定 error 类型队列，并指定 routingKey=error
                    //channel.QueueBind(queue: "DirectExchangeErrorQueue",
                    //          exchange: "DirectExChange",
                    //          routingKey: "error"); 
                    #endregion

                    //消费队列中的所有消息；                                   
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body.ToArray());
                        Console.WriteLine($"【{message}】，已发送邮件通知系统管理员~~");
                    };
                    //处理消息
                    channel.BasicConsume(queue: "DirectExchangeErrorQueue",
                                         autoAck: true,
                                         consumer: consumer);
                    Console.ReadLine(); 
                }
            }
        }


      
    }
}
