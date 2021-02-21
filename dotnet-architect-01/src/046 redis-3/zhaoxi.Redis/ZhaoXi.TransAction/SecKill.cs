using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZhaoXi.TransAction
{
	class SecKill
	{
		public static void Show()
		{
			using (RedisClient client = new RedisClient("127.0.0.1", 6379, "123456"))
			{
				//删除当前数据库中的所有Key  默认删除的是db0
				client.FlushDb();
				//删除所有数据库中的key 
				client.FlushAll();
				//库存数量
				client.Set<int>("number", 1); 
				//订单数量
				client.Set<int>("ordernumber", 0);
				var num = client.Get<int>("number");
				if (num >= 1)
				{
					//超卖了 
					//库存数量-1
					client.Decr("number");
					// +1
					client.Incr("ordernumber");

					Console.WriteLine("**********抢购成功***************");

				}
				else
				{
					Console.WriteLine("抢购失败");
				}
			}

		}
	}
}
