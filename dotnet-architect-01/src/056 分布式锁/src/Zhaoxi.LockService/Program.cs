using Microsoft.Extensions.Configuration;
using ServiceStack;
using ServiceStack.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Zhaoxi.LockService
{

	public class Userinfo {
		public int ID { get; set; }

		public string Name { get; set; }
	}

	class Program
	{
		static void Main(string[] args)
		{

			Userinfo o = new Userinfo();

			////命令行参数启动
			////dotnet Zhaoxi.LockService.dll --minute=18
			var builder = new ConfigurationBuilder().AddCommandLine(args);
			var configuration = builder.Build();
			int minute = int.Parse(configuration["minute"]);
			using (var client = new RedisClient("127.0.0.1", 6379, "123456"))
			{
				
				//票的库存
				client.Set<int>("inventoryNum", 10);
				//订单数
				client.Set<int>("orderNum", 0);
			}
			//开启10个线程去抢购
			Console.WriteLine($"在{minute}分0秒正式开启秒杀！");
			var flag = true;
			while (flag)
			{
				//循环到这个时间（分钟）的时候，我们可以开始抢票
				if (DateTime.Now.Minute == minute)
				{
					flag = false;
					Parallel.For(0, 30, (i) =>
					{
						int temp = i;
						Task.Run(() =>
						{
						    //NormalSecondsKill.Show();
							//BlockingLock.Show(i, "akey", TimeSpan.FromSeconds(100));
							ImmediatelyLock.Show(i, "akey", TimeSpan.FromSeconds(100));
						});
						//Thread.Sleep(100); 
					});
				}
			}


			Console.ReadKey();
		}
	}
}
