using System;

namespace Atomos.Tests.Pool
{
    public abstract partial class IPool_Generic_Test<TPool, T>
        where TPool : IPool<T>
        where T : class
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

        #region Items Validation

        protected abstract bool IsDisposed(T item);

        protected abstract bool IsReset(T item);

        #endregion
    }
}