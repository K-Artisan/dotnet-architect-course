using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Zhaoxi.LockService
{
	public class NormalSecondsKill
	{
		//private static Userinfo olock = new Userinfo();
		private static object olock = new object();

		public static void Show()
		{
			// 刚才开启了三个进程，每一个进程有30个线程
			//这个代码能控制多少个线程，，只控制了当前进程的30个线程

			// 获取 c盘的文件，如果里面有值，thread.sleep(999) 微循环，不停的去判断
			//当前的这个c盘的文件有没有值
			// 当没有值得时候，直接给里面写个值，然后执行下面的代码

			// 如果进来里面没有值，然后直接写值，执行代码
			// 前提是文本文件只能有一个人读，可以操作文件的时候

			// 比如现在能解决-- 当前的电脑上面，开启了三个进程，可以解决

			// 如果我的三个进程不在一个电脑上面呢？？？？

			// 数据库，创建一个表，有一个字段，提交事务，去操作

			//用mysql数据库可以解决吗？？？？


			// 我们的关系型数据库操作的是硬盘，性能会大大的降低

			// sqlserver  之前是没有内存数据库，现在也出了一个内存数据，不开源，花钱
			// 最新版本  能用mysql就用mysql,能不用就不用
			// servicestack.redis吗 我们公司用的额，性能高，如果你要用其他，去研究底层，重新写代码

			// 我们需要一个分布式而且是内存的数据库
			//lock (typeof(int)) //爱护自己的肾，但是慎用
			lock(olock)
			{// 去数据库中判断有没有数据   如果有数据 直接 return 
			 //如果没有内容，则在数据库中写点内容，然后执行下面的代码，代码执行完之后把数据库的内容清理掉
				using (var client = new RedisClient("127.0.0.1", 6379,"123456"))
				{

					//库存数量
					var inventory = client.Get<int>("inventoryNum");
					if (inventory > 0)
					{
						//给库存-1
						client.Set<int>("inventoryNum", inventory - 1);
						//订单数量 +1
						var orderNum = client.Incr("orderNum");

						Console.WriteLine($"抢购成功*****线程id：{ Thread.CurrentThread.ManagedThreadId.ToString("00")},库存：{inventory},订单数量：{orderNum}");
					}
					else
					{
						Console.WriteLine("抢购失败");
					}
				}
			}



		}
	}
}
