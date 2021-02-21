﻿using ServiceStack.Redis;
using System;

namespace RedisSubscribeDB
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				//消费者
				using (RedisClient consumer = new RedisClient("127.0.0.1", 6379,"123456"))
				{
					Console.WriteLine($"创建订阅异常信息数据库记录");
					var subscription = consumer.CreateSubscription();
					//接受到消息时
					subscription.OnMessage = (channel, msg) =>
					{
						if (msg != "CTRL:PULSE")
						{
							Console.WriteLine($"从频道：{channel}上接受到消息：{msg},时间：{DateTime.Now.ToString("yyyyMMdd HH:mm:sss")}");
							Logger.WriteLogByDB(msg);
							Console.WriteLine("_________________________________记录成功__________________________________");
						}
					};
					//订阅频道时
					subscription.OnSubscribe = (channel) =>
					{
						Console.WriteLine("订阅客户端：开始订阅" + channel);
					};
					//取消订阅频道时
					subscription.OnUnSubscribe = (a) => { Console.WriteLine("订阅客户端：取消订阅"); };

					//订阅频道
					string channelName = "Send_Log";
					subscription.SubscribeToChannels(channelName);
				}
			}
			catch (Exception ex)
			{

				Console.WriteLine(ex.Message);
			}
			
		}
	}
}