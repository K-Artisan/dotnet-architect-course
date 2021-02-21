using ServiceStack.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;
using ZhaoXiSeckillModel;

namespace ZhaoXi.Seckill_Server
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Task.Run(() =>
				{
					using (RedisClient client = new RedisClient("127.0.0.1", 6379))
					{

						OrderInfo orderInfo = new OrderInfo();
						while (true)
						{
							string listkey = "orderlist";
							var listvalue = client.RemoveEndFromList(listkey);
							//自己实现自己的业务
							//var listvalue = client.RemoveStartFromList(listkey);
							if (!string.IsNullOrWhiteSpace(listvalue))
							{
								var order = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderInfo>(listvalue);
								//去做业务处理
								Console.WriteLine($"消费完成：{order.ID}::::::{order.OrderTime}");
							}
							Thread.Sleep(1000);
						}

					}

				});

			}
			catch (Exception ex)
			{

				Console.WriteLine(ex.Message);
			}

			Console.ReadKey();
		}
	}
}
