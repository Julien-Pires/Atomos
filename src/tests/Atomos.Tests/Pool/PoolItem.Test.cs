using System;

namespace Atomos.Tests.Pool
{
    public interface IPoolItem_Test
    {
        #region Properties

        int Value { get; set; }

        bool IsDisposed { get; }

        #endregion
    }

    public class PoolItem_Test : IPoolItem_Test, IDisposable
    {
        #region Properties

        public int Value { get; set; }

        public bool IsDisposed { get; private set; }

        #endregion

        #region Cleanup

        public void Dispose()
        {
            IsDisposed = true;
        }

        #endregion
    }
}