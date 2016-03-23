using System;

using Atomos.Atomos;

namespace Atomos.Tests.Pool
{
    public abstract partial class IPool_Generic_Test_Flexible<TPool, T> : IPool_Generic_Test<TPool, T>
        where TPool : IPool<T>
        where T : class, IPoolItem_Test, new()
    {
        public IPool_Generic_Test_Flexible(Func<PoolSettings<T>, TPool> factory) : base(factory, PoolingMode.Flexible)
        {
        }
    }
}
