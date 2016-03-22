using System;

using Atomos.Atomos;

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

    public class PoolItem_Test : IPoolItem_Test
    {
        #region Properties

        public int Value { get; set; }

        public bool IsReset { get; private set; }

        public bool IsDisposed { get; private set; }

        #endregion

        #region Cleanup

        public void Dispose()
        {
            IsDisposed = true;
        }

        public void Reset()
        {
            IsReset = true;
        }

        #endregion
    }
}