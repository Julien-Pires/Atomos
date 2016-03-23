namespace Atomos.Tests.Pool
{
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