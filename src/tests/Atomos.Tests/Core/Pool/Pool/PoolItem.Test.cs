using System;

namespace Atomos.Tests.Pool
{
    public sealed class PoolItem_Test : IDisposable, IPoolItem
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
            Value = -1;
        }

        #endregion
    }
}