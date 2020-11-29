using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Orm.Framework
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// 提供事务
        /// </summary>
        /// <param name="action">多个数据操作</param>
        public void Invoke(Action action)
        {
            using(var trans = new TransactionScope())
            {
                action.Invoke();
                trans.Complete();
            }

        }

        public void Dispose()
        {
        }
    }
}
