using System;

namespace Atomos.Tests.Pool
{
    public interface IPoolItem_Test : IPoolItem, IDisposable
    {
        #region Properties

        int Value { get; set; }

        bool IsDisposed { get; }

        bool IsReset { get; }

        #endregion
    }
}
