﻿using System;
using ServiceStack.Redis;
namespace Zhaoxi.ELK.LogstashRedis
{
	class Program
	{
		static void Main(string[] args)
		{
			string listkey = "listlog";
			while (1 == 1)
			{
				Console.WriteLine("请输入发送的内容");
				var message = Console.ReadLine();
				using (RedisClient client = new RedisClient("127.0.0.1", 6379, "123456"))
				//using (RedisClient client = new RedisClient("127.0.0.1"))
				{
					client.AddItemToList(listkey, message);
				}
			}
		}
	}
}
