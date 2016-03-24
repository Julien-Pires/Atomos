using System;

using Atomos;

namespace Atomos.Tests.Pool
{
    public abstract partial class IPool_Generic_Test_Strict<TPool, T> : IPool_Generic_Test<TPool, T>
        where TPool : IPool<T>
        where T : class, IPoolItem_Test, new()
    {
        public IPool_Generic_Test_Strict(Func<PoolSettings<T>, TPool> factory) : base(factory, PoolingMode.Strict)
        {
        }
    }
}
