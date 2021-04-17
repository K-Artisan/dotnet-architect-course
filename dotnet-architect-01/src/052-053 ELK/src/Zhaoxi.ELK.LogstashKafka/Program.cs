using System;

namespace Zhaoxi.ELK.LogstashKafka
{
	class Program
	{
		static async System.Threading.Tasks.Task Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			while (1 == 1)
			{
				Console.WriteLine("请输入发送的内容");
				var message = Console.ReadLine();
				string brokerList = "39.96.82.51:9093";
				await ConfulentKafka.Produce(brokerList, "kafkalog", message);
			}
		}
	}
}
