﻿using NConsul;
using NConsul.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zhaoxi.AgilityFramework.ConsulClientExtend
{
    public interface IConsulIDistributed : IDisposable
    {
        void Show();
        Task<IDistributedLock> AcquireLock(string key);
        Task ExecuteLocked(string key, Action action);
    }

    public class ConsulIDistributed : IConsulIDistributed
    {
        public void Show()
        {
            using (ConsulClient client = new ConsulClient(c =>
             {
                 c.Address = new Uri($"http://127.0.0.1:{8500}/");
                 c.Datacenter = "dc1";
             }))
            {
                client.KV.Put(new KVPair("Eleven") { Value = Encoding.UTF8.GetBytes("This is Teacher") });
                Console.WriteLine(client.KV.Get("Eleven"));
                client.KV.Delete("Eleven");
            }
        }

        #region 分布式锁
        private static string prefix = "consullock_";  // 同步锁参数前缀
        private ConsulClient consulClient;

        public ConsulIDistributed()
        {
            this.consulClient = new ConsulClient(c =>
             {
                 c.Address = new Uri($"http://127.0.0.1:{8500}/");
                 c.Datacenter = "dc1";
             });
        }
        /// <summary>
        /// 需要先初始化
        /// </summary>
        /// <param name="key"></param>
        public Task<IDistributedLock> AcquireLock(string key)
        {
            LockOptions opts = new LockOptions($"{prefix}{key}");//默认值
            //{
            //    LockRetryTime = TimeSpan.FromSeconds(5),
            //    LockWaitTime = TimeSpan.FromSeconds(3),
            //    MonitorRetryTime = TimeSpan.FromSeconds(1)
            //};
            return this.consulClient.AcquireLock(opts);
        }

   

        /// <summary>
        /// 包装了一层，委托嵌套
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public Task ExecuteLocked(string key, Action action)
        {
            //Console.WriteLine($"{prefix}{key}");
            LockOptions opts = new LockOptions($"{prefix}{key}");//默认值
            //{
            //    LockRetryTime = TimeSpan.FromSeconds(5),
            //    LockWaitTime = TimeSpan.FromSeconds(3),
            //    MonitorRetryTime = TimeSpan.FromSeconds(1)
            //};
            return this.consulClient.ExecuteLocked(opts, action);
        }

        public void Dispose()
        {
            if (this.consulClient != null)
            {
                this.consulClient.Dispose();
            }
        }
        #endregion
    }
}
