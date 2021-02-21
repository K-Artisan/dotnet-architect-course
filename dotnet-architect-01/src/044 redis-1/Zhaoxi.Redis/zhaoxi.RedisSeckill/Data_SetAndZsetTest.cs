using Newtonsoft.Json;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisSeckill
{
	class Data_SetAndZsetTest
	{
		public static void Show()
		{
			using (RedisClient client = new RedisClient("127.0.0.1", 6379, "123456"))
			{   //删除当前数据库中的所有Key  默认删除的是db0
				client.FlushDb();
				//删除所有数据库中的key 
				client.FlushAll();
				#region Set 不重复集合
				string key = "set_key";

                #region 添加键值 //就是自动去重，再带去重的功能
                //var litaibai = new UserInfo() { Id = 1, Name = "李太白" };
                //            //client.AddItemToList(key, JsonConvert.SerializeObject(litaibai));
                //            //client.AddItemToList(key, JsonConvert.SerializeObject(litaibai));


                //            client.AddItemToSet(key, JsonConvert.SerializeObject(litaibai));
                //            litaibai.Name = "李太白2";
                //            client.AddItemToSet(key, JsonConvert.SerializeObject(litaibai));
                //            client.AddItemToSet(key, JsonConvert.SerializeObject(litaibai));
                //            client.AddItemToSet(key, JsonConvert.SerializeObject(litaibai));
                //            Console.WriteLine("***完成了");
                #endregion

                #region 随机获取key集合中的一个值，获取当前setid中的所有值
                ////批量的去操作set 集合
                //Console.WriteLine("set 开始了");
                //client.AddRangeToSet(key, new List<string>() { "001", "001", "002", "003", "003", "004" });
                //////当前setid中的值数量
                //Console.WriteLine(client.GetSetCount(key)); //4(001,003去重)
                ////随机获取key集合中的一个值
                //Console.WriteLine(client.GetRandomItemFromSet(key));
                ////获取当前setid中的所有值
                //var lists = client.GetAllItemsFromSet(key); 
                //Console.WriteLine("展示所有的值");
                //foreach (var item in lists)
                //{
                //    Console.WriteLine(item); //002,004,003,001
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
                //    Console.WriteLine(item);
                //}
                #endregion

                #region 根据小key 删除
                //client.AddRangeToSet(key, new List<string>() { "001", "001", "002" });
                //client.RemoveItemFromSet(key, "001");
                //var lists = client.GetAllItemsFromSet(key);
                //Console.WriteLine("展示删除之后所有的值");
                //foreach (var item in lists)
                //{
                //    Console.WriteLine(item); //002
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
                //    Console.WriteLine(item); //002,001,003
                //}

                //Console.WriteLine("tokey data ~~~~~~");
                //foreach (var item in client.GetAllItemsFromSet("tokey"))
                //{
                //    Console.WriteLine(item); //004,001,002
                //}
                #endregion

                #region 并集  把两个集合合并起来，然后去重

                //client.AddRangeToSet("keyone", new List<string>() { "001", "002", "003", "004" });
                //client.AddRangeToSet("keytwo", new List<string>() { "001", "002", "005" });
                //var unionlist = client.GetUnionFromSets("keyone", "keytwo");
                //Console.WriteLine("返回并集结果");
                //foreach (var item in unionlist)
                //{
                //    Console.WriteLine(item);//001,002,003,004,005
                //}
                ////把 keyone 和keytwo 并集结果存放到newkey 集合中
                //client.StoreUnionFromSets("newkey", "keyone", "keytwo");
                //Console.WriteLine("返回并集结果的新集合数据");
                //foreach (var item in client.GetAllItemsFromSet("newkey"))
                //{
                //    Console.WriteLine(item);//001,002,003,004,005
                //}
                #endregion

                #region 交集 获取两个集合中共同存在的元素
                //client.AddRangeToSet("keyone", new List<string>() { "001", "002", "003", "004" });
                //client.AddRangeToSet("keytwo", new List<string>() { "001", "002", "005" });
                //var Intersectlist = client.GetIntersectFromSets("keyone", "keytwo");
                //Console.WriteLine("交集的结果");
                //foreach (var item in Intersectlist)
                //{
                //    Console.WriteLine(item); //001, 002
                //}
                ////把 keyone 和keytwo 交集结果存放到newkey 集合中
                //client.StoreIntersectFromSets("newkey", "keyone", "keytwo"); //生成新的集合
                //Console.WriteLine("返回交集结果的新集合数据");
                //foreach (var item in client.GetAllItemsFromSet("newkey"))
                //{
                //    Console.WriteLine(item); //001, 002
                //}
                #endregion

                #endregion

                #region  sorted set
                //string zsett_key = "zset_key";
                ////添加一个kye
                //client.AddItemToSortedSet(zsett_key, "cc", 33); //cc是值，33是排序号
                //client.AddItemToSortedSet(zsett_key, "cc", 45); //cc是值，45是排序号，根据值'cc'去重，排序号被覆盖为45

                //Console.WriteLine("ok");
                ////获取当前value的结果
                //Console.WriteLine(client.GetItemIndexInSortedSet(zsett_key, "cc")); //0
                ////批量操作多个key ，给多个key 赋值1
                //client.AddRangeToSortedSet(zsett_key, new List<string>() { "a", "b" }, 1);

                //foreach (var item in client.GetAllItemsFromSortedSet(zsett_key))
                //{
                //    Console.WriteLine(item); //a,bcc
                //}
                //client.AddItemToSortedSet("蜀国", "刘备", 5);
                //client.AddItemToSortedSet("蜀国", "关羽", 2);
                //client.AddItemToSortedSet("蜀国", "张飞", 3);
                //client.AddItemToSortedSet("魏国", "刘备", 5);
                //client.AddItemToSortedSet("魏国", "关羽", 2);
                //client.AddItemToSortedSet("蜀国", "张飞", 3);
                ////获取 key为蜀国的下标 0，到2 并且排序
                //IDictionary<String, double> Dic = client.GetRangeWithScoresFromSortedSet("蜀国", 0, 2);
                //foreach (var r in Dic)
                //{
                //    Console.WriteLine(r.Key + ":" + r.Value); //排序获取，输出顺序为：关羽:2, 张飞:3, 刘备:5
                //}

                //var DicString = client.StoreIntersectFromSortedSets("2", "蜀国", "魏国");
                //var ss = client.GetAllItemsFromSortedSet("2");
                //foreach (var r in DicString)
                //{
                //    Console.WriteLine(r.Key + ":" + r.Value);
                //}
                #endregion
            }
		}
	}
}
