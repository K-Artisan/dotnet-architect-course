using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServiceStack.Redis;
namespace RedisChapter1
{
	/// <summary>
	/// 秒杀
	/// </summary>
	public class Seckill
	{
		public static void Show(string id, int minute)
		{
			#region 自减1，返回自减后的值
			//开启10个线程去抢购
			Console.WriteLine($"在{minute}分0秒正式开启秒杀！");
			var flag = true;
			while (flag)
			{
				if (DateTime.Now.Minute == minute)
				{
					Console.WriteLine("秒杀开始-----------");
					flag = false;
					for (int i = 0; i < 10; i++)
					{
						string name = $"客户端{id}，{i}号用户：";
						Task.Run(() =>
						{
							using (RedisClient client = new RedisClient("127.0.0.1", 6379,"123456"))
							{
								
								//本来是二步走
								//1 订单+1
								//2 库存-1 //同一个时间片。只会执行一条指令
								var num = client.Decr("number");
								if (num < 0)
								{
									Console.WriteLine(name + "抢购失败");
								}
								//>=0
								else
								{
									Console.WriteLine(name + "**抢购成功**");
								}
							}
						});
						Thread.Sleep(10);
					}
				}
				Thread.Sleep(10);
			}
			Console.WriteLine("秒杀结束-----------");
			Console.ReadLine();
			#endregion
		}
	}
}
