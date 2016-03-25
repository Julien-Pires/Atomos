using System;

namespace Atomos.Tests.Pool
{
    public abstract partial class Pool_Generic_Test<TPool, T> : IPool_Generic_Test<TPool, T>
        where TPool : IPool<T>
        where T : class, IPoolItem_Test, new()
    {
        #region Properties

        public PoolingMode Mode { get; }

        #endregion

        #region Constructors

        protected Pool_Generic_Test(Func<PoolSettings<T>, TPool> factory, PoolingMode mode) : 
            base(() => factory(new PoolSettings<T> { Mode = mode }))
        {
            Mode = mode;
        }

        #endregion
    }
}