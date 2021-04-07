using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.RabbitMQ.MessageProducer.ExchangeDemo
{
    public class TopicExchange
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
                    //1.定义主题类型的交换机
                    channel.ExchangeDeclare(exchange: "TopicExchange", type: ExchangeType.Topic, durable: true, autoDelete: false, arguments: null); 

                    // 2.定义队列
                    channel.QueueDeclare(queue: "ChinaQueue", durable: true, exclusive: false, autoDelete: false, arguments: null); 
                    channel.QueueDeclare(queue: "newsQueue", durable: true, exclusive: false, autoDelete: false,  arguments: null);
                    channel.QueueDeclare(queue: "weatherQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);


                    //----------绑定交换机和队列---------
                    //3.1.匹配 routingKey: "China.#" 的消息进入队列 ChinaQueue
                    channel.QueueBind(queue: "ChinaQueue", exchange: "TopicExchange", routingKey: "China.#", arguments: null);

                    //3.2.匹配 routingKey: "#.news" 的消息进入队列 newsQueue
                    channel.QueueBind(queue: "newsQueue", exchange: "TopicExchange", routingKey: "#.news", arguments: null);

                    //3.3.匹配 routingKey: "#.weather" 的消息进入队列 weatherQueue
                    channel.QueueBind(queue: "weatherQueue", exchange: "TopicExchange", routingKey: "#.weather", arguments: null);

                    //4.发送消息：各种路由键的各个消息
                    {
                        //"China.news"
                        string message = "来自中国的新闻消息。。。。";
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: "TopicExchange", routingKey: "China.news", basicProperties: null, body: body);
                        Console.WriteLine($"消息【{message}】已发送到队列");
                    }

                    {
                        //routingKey: "China.weather"
                        string message = "来自中国的天气消息。。。。";
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: "TopicExchange", routingKey: "China.weather", basicProperties: null, body: body);
                        Console.WriteLine($"消息【{message}】已发送到队列");
                    }
                    {
                        //routingKey: "usa.news" 
                        string message = "来自美国的新闻消息。。。。";
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: "TopicExchange", routingKey: "usa.news", basicProperties: null, body: body);
                        Console.WriteLine($"消息【{message}】已发送到队列");
                    } 
                    {
                        //routingKey: "usa.weather"
                        string message = "来自美国的天气消息。。。。";
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: "TopicExchange", routingKey: "usa.weather", basicProperties: null, body: body);
                        Console.WriteLine($"消息【{message}】已发送到队列");
                    }

                    {
                        //----------routingKey: "xxx.yyyy",将被丢弃，因为没有队列与其匹配----------
                        string message = "垃圾数据。。。。";
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: "TopicExchange", routingKey: "xxx.yyyy", basicProperties: null, body: body);
                        Console.WriteLine($"消息【{message}】，将被丢弃，因为没有队列与其匹配");
                    }
                }
            }
        }
    }
}
