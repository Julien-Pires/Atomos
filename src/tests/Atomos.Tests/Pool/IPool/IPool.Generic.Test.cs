using System;

namespace Atomos.Tests.Pool
{
    public abstract partial class IPool_Generic_Test<TPool, T>
        where TPool : IPool<T>
        where T : class, IPoolItem_Test, new()
    {
        #region Fields

        protected readonly TPool Pool;

        #endregion

        #region Constructors

        protected IPool_Generic_Test(Func<TPool> factory)
        {
            Pool = factory();
        }

        #endregion
    }
}