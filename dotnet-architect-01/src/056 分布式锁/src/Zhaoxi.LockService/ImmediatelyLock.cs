using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Zhaoxi.LockService
{
	public class ImmediatelyLock
	{
		public static void Show(int i, string key, TimeSpan timeout)
		{
			using (var client = new RedisClient("127.0.0.1", 6379, "123456"))
			{
				// 非阻塞加锁   如果已经存在当前的key，则执行失败，然后false
				// 把这个时间 timeout 设置长不就行了吗，但是你需要悠着点
				//没有完全之策 ，一般在生产环境，给一个不要超过3s就可以



				bool isLocked = client.Add<string>("DataLock:" + key, key, timeout);
				if (isLocked)
				{
					try
					{
						//库存数量
						var inventory = client.Get<int>("inventoryNum");
						if (inventory > 0)
						{
							client.Set<int>("inventoryNum", inventory - 1);
							//订单数量
							var orderNum = client.Incr("orderNum");
							Console.WriteLine($"{i}抢购成功*****线程id：{ Thread.CurrentThread.ManagedThreadId.ToString("00")},库存：{inventory},订单数量：{orderNum}");
						}
						else
						{
							Console.WriteLine($"{i}抢购失败:原因，没有库存");
						}
					}
					catch
					{
						throw;
					}
					finally
					{
						client.Remove("DataLock:" + key);
					}
				}

				else
				{
					Console.WriteLine($"{i}抢购失败：原因：没有拿到锁");
				}
			}

		}
	}
}
