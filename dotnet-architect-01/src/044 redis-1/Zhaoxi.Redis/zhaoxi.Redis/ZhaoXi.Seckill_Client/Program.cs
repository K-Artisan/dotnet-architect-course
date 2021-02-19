using Microsoft.Extensions.Configuration;
using ServiceStack.Redis;
using System;
using ZhaoXiSeckillModel;

namespace ZhaoXi.Seckill_Client
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{//命令行参数启动
			 //dotnet ZhaoXi.Seckill_Client.dll --minute=31
				var builder = new ConfigurationBuilder().AddCommandLine(args);
				var configuration = builder.Build();
				string id = "";
				using (RedisClient client = new RedisClient("127.0.0.1", 6379))
				{
					id = client.Incr("id").ToString();
				}
				int minute = int.Parse(configuration["minute"]);
				Console.WriteLine("开始" + id);
				Seckill.Show(id, minute);

			}
			catch (Exception ex)
			{

				Console.WriteLine(ex.Message);
			}

		}
	}
}
