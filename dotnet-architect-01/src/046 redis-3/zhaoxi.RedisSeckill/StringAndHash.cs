using Newtonsoft.Json;
using RedisSeckill;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisChapter1
{
	class StringAndHash
	{
		public static void Show()
		{
			using (RedisClient client = new RedisClient("127.0.0.1", 6379))
			{
				//删除当前数据库中的所有Key  默认删除的是db0
				client.FlushDb();
				//删除所有数据库中的key 
				client.FlushAll();
				//用户有好几个字段属性，id，name ，number 
				// 可能会发生一些其他操作
				UserInfo zhangsan = new UserInfo() { Id = 1, Name = "张三", number = 0 };
				client.Set<string>("userinfo", JsonConvert.SerializeObject(zhangsan));
				var newvalue = JsonConvert.DeserializeObject<UserInfo>(client.Get<string>("userinfo"));
				newvalue.number++;
				client.Set<string>("userinfo", JsonConvert.SerializeObject(newvalue));



				client.StoreAsHash<UserInfo>(new UserInfo() { Id = 1, Name = "clay", number = 0 });
				client.SetEntryInHash("userinfo", "number", "1");

				//HashTool.StoreAsHash<UserInfoTwo>(new UserInfoTwo() { Id = "10001", Name = "clay" });
				//var user = HashTool.GetFromHash<UserInfoTwo>("10001");

				Console.WriteLine("华丽丽的结束");
			}




		}
	}
}