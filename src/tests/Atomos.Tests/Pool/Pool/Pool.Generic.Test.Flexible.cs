using System;

namespace Atomos.Tests.Pool
{
    public abstract partial class Pool_Generic_Test_Flexible<TPool, T> : Pool_Generic_Test<TPool, T>
        where TPool : Pool<T>, IPool<T>
        where T : class, IPoolItem_Test, new()
    {
        protected Pool_Generic_Test_Flexible(Func<PoolSettings<T>, TPool> factory) : base(factory, PoolingMode.Flexible)
        {
        }
    }
}