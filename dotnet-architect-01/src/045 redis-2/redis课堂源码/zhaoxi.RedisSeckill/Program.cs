using Microsoft.Extensions.Configuration;
using RedisSeckill;
using ServiceStack.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RedisChapter1
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{




				#region String
				{
				//	Console.WriteLine("*****************String****************");
				//	Data_StringTest.Show();
				}
				#endregion

				#region 秒杀
				{
					//////命令行参数启动
					////dotnet RedisSeckill.dll --id=1 minute=18
					//var builder = new ConfigurationBuilder().AddCommandLine(args);
					//var configuration = builder.Build();
					//string id = configuration["id"];
					//int minute = int.Parse(configuration["minute"]);
					//Console.WriteLine("开始" + id);

					//using (RedisClient client = new RedisClient("127.0.0.1", 6379))
					//{
					//	//首先给数据库预支了秒杀商品的数量
					//	client.Set<int>("number", 10);
					//}
					//Seckill.Show(id, minute);

				}
				#endregion
				#region Hash
				{
					//Console.WriteLine("*****************Hash****************");
					//Data_HashTest.Show();
				}
				#endregion

				#region Set ZSet
				{
					//Console.WriteLine("*****************Set ZSet****************");
					//Data_SetAndZsetTest.Show();
				}
				#endregion
				#region List
				{
					Console.WriteLine("*****************List****************");
					Data_ListTest.Show();
				}
				#endregion
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			Console.ReadLine();
		}
	}
}
