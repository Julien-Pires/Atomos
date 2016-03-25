using System;

namespace Atomos.Tests.Pool
{
    public abstract partial class Pool_Generic_Test_Strict<TPool, T> : Pool_Generic_Test<TPool, T>
        where TPool : IPool<T>
        where T : class, IPoolItem_Test, new()
    {
        protected Pool_Generic_Test_Strict(Func<PoolSettings<T>, TPool> factory) : base(factory, PoolingMode.Strict)
        {
        }
    }
}