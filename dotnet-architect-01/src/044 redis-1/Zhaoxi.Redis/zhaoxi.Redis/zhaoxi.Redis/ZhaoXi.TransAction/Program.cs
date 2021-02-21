using ServiceStack.Redis;
using System;

namespace ZhaoXi.TransAction
{
	class Program
	{
		static void Main(string[] args)
		{
			 
			try
			{
				////秒杀拆分
				//{
				//	SecKill.Show();
				//}

				//事务模式 
				using (RedisClient client = new RedisClient("127.0.0.1", 6379))
				{
					//删除当前数据库中的所有Key  默认删除的是db0
					client.FlushDb();
					//删除所有数据库中的key 
					client.FlushAll();

					client.Set("a", "1");
					client.Set("b", "1");
					client.Set("c", "1");

					//获取当前这三个key的版本号 实现事务
					client.Watch("a", "b", "c");
					using (var trans = client.CreateTransaction())
					{
						trans.QueueCommand(p => p.Set("a", "3"));
						trans.QueueCommand(p => p.Set("b", "3"));
						trans.QueueCommand(p => p.Set("c", "3"));
						//提交事务  如果在触发事务过程中，其他进程操作了当前的key则，事务提交失败，就是没有指令没有修改成功
						var flag = trans.Commit();
						// ID KEY
						Console.WriteLine(flag);
					}
					//根据key取出值，返回string
					Console.WriteLine(client.Get<string>("a") + ":" + client.Get<string>
					("b") + ":" + client.Get<string>
					("c"));
					Console.ReadLine();
				}


			}
			catch (Exception ex)
			{

				Console.WriteLine(ex.Message);
			}
			Console.ReadLine();
		}
	}
}
