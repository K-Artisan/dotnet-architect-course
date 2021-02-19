using System;
using System.Collections.Generic;
using System.Text;

namespace RedisChapter1
{
	class Temp
	{
		public void fun()
		{
			


			//UserInfo zhangsan = new UserInfo() { Id = 1, Name = "张三", number = 0 };
			//client.Set<string>("userinfo", JsonConvert.SerializeObject(zhangsan));
			//var newvalue = JsonConvert.DeserializeObject<UserInfo>(client.Get<string>("userinfo"));
			//client.Set<string>("userinfo", JsonConvert.SerializeObject(newvalue));
			//Console.WriteLine(newvalue.number);
			//newvalue.number++;
			//用户有好几个字段属性，id，name ，number 
			// 可能会发生一些其他操作

			

			#region List 

			string listid = "clay_list";

			#region 顺序添加
			//var caocao = new UserInfo() { Id = 1, Name = "李太白" };
			//client.AddItemToList(listid, JsonConvert.SerializeObject(caocao));
			//var jiaxu = new UserInfo() { Id = 2, Name = "贾诩" };
			//client.AddItemToList(listid, JsonConvert.SerializeObject(jiaxu));
			#endregion

			#region 从左侧向list中添加值 追加
			//var liubei = new UserInfo() { Id = 1, Name = "刘备" };
			//client.PushItemToList(listid, JsonConvert.SerializeObject(liubei));
			#endregion

			#region 从左侧向list中添加值，并设置过期时间
			//var caocao = new UserInfo() { Id = 1, Name = "李太白" };
			//client.AddItemToList(listid, JsonConvert.SerializeObject(caocao));
			//var liubei = new UserInfo() { Id = 2, Name = "刘备" };
			//client.PushItemToList(listid, JsonConvert.SerializeObject(liubei));
			////只在内存中存储一秒
			//client.ExpireEntryAt(listid, DateTime.Now.AddSeconds(1));
			//Console.WriteLine(client.GetListCount(listid));
			//Task.Delay(1 * 1000).Wait();
			//Console.WriteLine("1秒之后");
			//Console.WriteLine(client.GetListCount(listid));
			//雪崩 问题：瞬间大量的数据消失-》大量的数据不要一下的全部消失
			#endregion

			#region 从右侧向list中添加值，并设置过期时间 插队
			//var caocao = new UserInfo() { Id = 1, Name = "李太白" };
			//client.AddItemToList(listid, JsonConvert.SerializeObject(caocao));
			//var jiaxu = new UserInfo() { Id = 2, Name = "贾诩" };
			//client.AddItemToList(listid, JsonConvert.SerializeObject(jiaxu));
			//var gaunyu = new UserInfo() { Id = 3, Name = "关羽" };
			////向右追加就是插队
			//client.PrependItemToList(listid, JsonConvert.SerializeObject(gaunyu));
			//var caomegndeng = new UserInfo() { Id = 3, Name = "曹孟德" };
			//client.PrependItemToList(listid, JsonConvert.SerializeObject(caomegndeng));
			//client.ExpireEntryAt(listid, DateTime.Now.AddSeconds(1));
			//Console.WriteLine(client.GetListCount(listid));
			//Task.Delay(1 * 1000).Wait();
			//Console.WriteLine("1秒之后");
			//Console.WriteLine(client.GetListCount(listid));
			#endregion

			#region  为key添加多个值
			//client.AddRangeToList(listid, new List<string>() { "001", "002", "003", "004" });
			////批量去读取list中的元素
			//var lists = client.GetAllItemsFromList(listid);
			//foreach (var item in lists)
			//{
			//	Console.WriteLine(item);
			//}
			#endregion

			#region 获取key中下标为star到end的值集合
			//client.AddRangeToList(listid, new List<string>() { "001", "002", "003", "004" });
			//var lists = client.GetRangeFromList(listid, 0, 1);//从下标0到1的值
			//foreach (var item in lists)
			//{
			//	Console.WriteLine(item);
			//}
			#endregion

			#region  list 队列和集合操作
			//var caocao = new UserInfo() { Id = 1, Name = "李太白" };
			//client.AddItemToList(listid, JsonConvert.SerializeObject(caocao));
			//var jiaxu = new UserInfo() { Id = 2, Name = "贾诩" };
			//client.AddItemToList(listid, JsonConvert.SerializeObject(jiaxu));
			//var gaunyu = new UserInfo() { Id = 3, Name = "关羽" };
			//client.AddItemToList(listid, JsonConvert.SerializeObject(gaunyu));
			//移除尾部 并返回移除的数据
			//Console.WriteLine(client.RemoveEndFromList(listid));
			//foreach (var item in client.GetAllItemsFromList(listid))
			//{
			//	Console.WriteLine(JsonConvert.DeserializeObject<UserInfo>(item).Name);
			//}
			//移除头部并返回移除的数据
			//client.RemoveStartFromList(listid);
			//foreach (var item in client.GetAllItemsFromList(listid))
			//{
			//	Console.WriteLine(JsonConvert.DeserializeObject<UserInfo>(item).Name);
			//}

			//从一个list的尾部移除一个数据，添加到另外一个list的头部，并返回移动的值
			//Console.WriteLine(client.PopAndPushItemBetweenLists(listid, "newlist"));
			//Console.WriteLine("移动之后新队列的元素结果");
			//Console.WriteLine(client.GetItemFromList("newlist",0));
			//根据下标获取想要的集合元素，不做移除操作
			//var userstr = client.GetItemFromList(listid, 0);
			//Console.WriteLine(JsonConvert.DeserializeObject<UserInfo>(userstr).Name);
			////修改当前下标的结果
			//client.SetItemInList(listid, 0, "new value");

			#endregion


			#endregion

			#region Set 不重复集合
			string key = "clay_set";

			#region 添加键值 //就是自动去重，再带去重的功能
			//var litaibai = new UserInfo() { Id = 1, Name = "李太白" };
			//脚下留心：不同的数据结构操作的时候，大key不可以重复
			//client.AddItemToList(key, JsonConvert.SerializeObject(litaibai));
			//client.AddItemToList(key, JsonConvert.SerializeObject(litaibai));
			//client.AddItemToSet(key, JsonConvert.SerializeObject(litaibai));
			//client.AddItemToSet(key, JsonConvert.SerializeObject(litaibai));
			//client.AddItemToSet(key, JsonConvert.SerializeObject(litaibai));
			//client.AddItemToSet(key, JsonConvert.SerializeObject(litaibai));
			#endregion

			#region 随机获取key集合中的一个值，获取当前setid中的所有值
			//批量的去操作set 集合
			//Console.WriteLine("set 开始了");
			//client.AddRangeToSet(key, new List<string>() { "001", "001", "002","003","003","004" });
			////当前setid中的值数量
			//Console.WriteLine(client.GetSetCount(key));
			////随机获取key集合中的一个值
			//Console.WriteLine(client.GetRandomItemFromSet(key));
			//获取当前setid中的所有值
			//var lists = client.GetAllItemsFromSet(key);
			//Console.WriteLine("展示所有的值");
			//foreach (var item in lists)
			//{
			//	Console.WriteLine(item);
			//}
			#endregion

			#region 随机删除key集合中的一个值
			//client.AddRangeToSet(key, new List<string>() { "001", "001", "002" });
			//////随机删除key集合中的一个值
			//Console.WriteLine("随机删除的值" + client.PopItemFromSet(key));
			//var lists = client.GetAllItemsFromSet(key);
			//Console.WriteLine("展示删除之后所有的值");
			//foreach (var item in lists)
			//{
			//	Console.WriteLine(item);
			//}
			#endregion

			#region 根据小key 删除
			//client.AddRangeToSet(key, new List<string>() { "001", "001", "002" });
			//client.RemoveItemFromSet(key, "001");
			//var lists = client.GetAllItemsFromSet(key);
			//Console.WriteLine("展示删除之后所有的值");
			//foreach (var item in lists)
			//{
			//	Console.WriteLine(item);
			//}
			#endregion

			#region 从fromkey集合中移除值为value的值，并把value添加到tokey集合中
			//client.AddRangeToSet("fromkey", new List<string>() { "003", "001", "002", "004" });
			//client.AddRangeToSet("tokey", new List<string>() { "001", "002" });
			////从fromkey 中把元素004 剪切到tokey 集合中去
			//client.MoveBetweenSets("fromkey", "tokey", "004");
			//Console.WriteLine("fromkey data ~~~~~~");
			//foreach (var item in client.GetAllItemsFromSet("fromkey"))
			//{
			//	Console.WriteLine(item);
			//}

			//Console.WriteLine("tokey data ~~~~~~");
			//foreach (var item in client.GetAllItemsFromSet("tokey"))
			//{
			//	Console.WriteLine(item);
			//}
			#endregion

			#region 并集  把两个集合合并起来，然后去重

			//client.AddRangeToSet("keyone", new List<string>() { "001", "002", "003", "004" });
			//client.AddRangeToSet("keytwo", new List<string>() { "001", "002", "005" });
			//var unionlist = client.GetUnionFromSets("keyone", "keytwo");
			//Console.WriteLine("返回并集结果");
			//foreach (var item in unionlist)
			//{
			//	Console.WriteLine(item);
			//}
			//把 keyone 和keytwo 并集结果存放到newkey 集合中
			//client.StoreUnionFromSets("newkey", "keyone", "keytwo");
			//Console.WriteLine("返回并集结果的新集合数据");
			//foreach (var item in client.GetAllItemsFromSet("newkey"))
			//{
			//	Console.WriteLine(item);
			//}
			#endregion

			#region 交集 获取两个集合中共同存在的元素
			//client.AddRangeToSet("keyone", new List<string>() { "001", "002", "003", "004" });
			//client.AddRangeToSet("keytwo", new List<string>() { "001", "002", "005" });
			//var Intersectlist = client.GetIntersectFromSets("keyone", "keytwo");
			//Console.WriteLine("交集的结果");
			//foreach (var item in Intersectlist)
			//{
			//	Console.WriteLine(item);
			//}
			////把 keyone 和keytwo 交集结果存放到newkey 集合中
			//client.StoreIntersectFromSets("newkey", "keyone", "keytwo");
			//Console.WriteLine("返回交集结果的新集合数据");
			//foreach (var item in client.GetAllItemsFromSet("newkey"))
			//{
			//	Console.WriteLine(item);
			//}
			#endregion

			#endregion

			#region  sorted set
			//string zsett_key = "clay_zset";
			////添加一个kye
			//client.AddItemToSortedSet(zsett_key, "cc", 33);
			////获取当前value的结果
			//Console.WriteLine(client.GetItemIndexInSortedSet(zsett_key, "cc"));
			////批量操作多个key ，给多个key 赋值1
			//client.AddRangeToSortedSet(zsett_key, new List<string>() { "a", "b" }, 1);

			//foreach (var item in client.GetAllItemsFromSortedSet(zsett_key))
			//{
			//	Console.WriteLine(item);
			//}


			//client.AddItemToSortedSet("蜀国", "刘备", 5);
			//client.AddItemToSortedSet("蜀国", "关羽", 2);
			//client.AddItemToSortedSet("蜀国", "张飞", 3);
			//client.AddItemToSortedSet("魏国", "刘备", 5);
			//client.AddItemToSortedSet("魏国", "关羽", 2);
			//client.AddItemToSortedSet("蜀国", "张飞", 3);
			////获取 key为蜀国的下标 0，到2
			//IDictionary<String, double> Dic = client.GetRangeWithScoresFromSortedSet("蜀国", 0, 2);
			//foreach (var r in Dic)
			//{
			//	Console.WriteLine(r.Key + ":" + r.Value);    //输出 
			//}
			//var DicString = client.StoreIntersectFromSortedSets("2", "蜀国", "魏国");
			//var ss = client.GetAllItemsFromSortedSet("2");
			//foreach (var r in DicString)
			//{
			//	Console.WriteLine(r.Key + ":" + r.Value);    //输出 
			//}
			#endregion
		}
	}
}
