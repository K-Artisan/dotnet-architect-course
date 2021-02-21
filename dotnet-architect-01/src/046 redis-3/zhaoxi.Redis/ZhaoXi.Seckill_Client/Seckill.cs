using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZhaoXiSeckillModel;

namespace ZhaoXi.Seckill_Client
{
	class Seckill
	{
		public static void Show(string id, int minute)
		{
			#region  
			//开启10个线程去抢购
			Console.WriteLine($"在{minute}分0秒正式开启秒杀！");
			var flag = true;
			while (flag)
			{
				if (DateTime.Now.Minute == minute)
				{
					flag = false;
					for (int i = 1; i <=100; i++)
					{

						string name = $"客户端{id}号:{i}";
						Task.Run(() =>
						{
							using (RedisClient client = new RedisClient("127.0.0.1", 6379))
							{
								OrderInfo orderInfo = new OrderInfo()
								{
									ID = name,
									OrderTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
								};
								string listkey = "orderlist";
								client.AddItemToList("orderlist", Newtonsoft.Json.JsonConvert.SerializeObject(orderInfo));
								Console.WriteLine($"{name}下单成功");
							}
						});
						Thread.Sleep(10);
					}
				}
				Thread.Sleep(10);
			}
			Console.ReadLine();
			#endregion
		}
	}
}
