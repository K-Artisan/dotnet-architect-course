using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ServiceStack.Redis;
namespace Zhaoxi.LockService
{
    public class BlockingLock
    {


        //这个是阻塞的锁
        public static void Show(int i, string key, TimeSpan timeout)
        {
            using (var client = new RedisClient("127.0.0.1", 6379, "123456"))
            {

                //只是加了这么一句话
                // 如果有一个先进去了，写入了key:DataLock+key。它执行完需要200毫秒

                // 同时来了三个线程，他们发现 key里面有值了，循环等待，什么时候，key里面没有值，然后自己可以执行代码

                // 用到了微循环，那必然要给一个限制
                //timeout 这个参数的意义，我最多等待这么长时间，如果拿不到，则我不需要在等待了
                //所以我们设置这个时间的时候，要合理一些

                // 这个时间还有一个意义
                // 表示，当前拿到锁进去的这个线程，最多能活多久，这个锁的存活的时间
                //规定时间，还没出来，就把锁释放掉



                //他没循环完，就是释放了锁。。 

                // 加了这句话，下面所有的代码都是单线程的执行
                using (var datalock = client.AcquireLock("DataLock:" + key, timeout))
                {
                    //库存数量
                    var inventory = client.Get<int>("inventoryNum");
                    if (inventory > 0)
                    {
                        client.Set<int>("inventoryNum", inventory - 1);
                        //订单数量
                        var orderNum = client.Incr("orderNum");
                        Console.WriteLine($"{i}抢购成功*****线程id：{ Thread.CurrentThread.ManagedThreadId.ToString("00")},库存：{inventory},订单数量：{orderNum}");
                    }
                    else
                    {
                        Console.WriteLine($"{i}抢购失败");
                    }

                    //client.Remove("DataLock:" + key);
                    Thread.Sleep(300);

                }

            }

        }
    }
}
