using System;
using System.Collections.Generic;
using System.Text;

namespace Orm.Framework
{
    public interface IUnitOfWork : IDisposable
    {
        void Invoke(Action action);
    }
}
