using System;
using System.Collections.Generic;
using System.Text;

namespace Zhaoxi.ArchitectBattalion.Framework
{
    public interface IUnitOfWork : IDisposable
    {
        void Invoke(Action action);
    }
}
