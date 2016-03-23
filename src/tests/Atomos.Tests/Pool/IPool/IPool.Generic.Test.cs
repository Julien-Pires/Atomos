using System;

using Atomos.Atomos;

namespace Atomos.Tests.Pool
{
    public abstract partial class IPool_Generic_Test<TPool, T>
        where TPool : IPool<T>
        where T : class, IPoolItem_Test, new()
    {
        #region Fields

        protected readonly TPool Pool;

        #endregion

        #region Properties

        public PoolingMode Mode { get; }

        #endregion

        #region Constructors

        protected IPool_Generic_Test(Func<PoolSettings<T>, TPool> factory, PoolingMode mode)
        {
            Pool = factory(new PoolSettings<T> { Mode = mode });
            Mode = mode;
        }

        #endregion
    }
}