using System;

using Atomos.Atomos;

using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class IPool_Generic_Test<TPool, T>
    {
        [Fact]
        public void Cast_Success()
        {
            PoolItem<T> item = Pool.Get();

            Assert.NotNull((T)item);
        }

        [Fact]
        public void Cast_AfterDisposed_ThrowException()
        {
            PoolItem<T> item = Pool.Get();
            item.Dispose();

            Assert.Throws<InvalidOperationException>(() => (T)item);
        }
    }
}
