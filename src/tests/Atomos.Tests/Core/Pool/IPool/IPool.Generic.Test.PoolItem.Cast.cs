using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class IPool_Generic_Test<TPool, T>
    {
        [Fact]
        public void Cast_Success()
        {
            TPool pool = Build();

            PoolItem<T> item = pool.Get();

            Assert.NotNull((T)item);
        }

        [Fact]
        public void Cast_AfterDisposed_ThrowException()
        {
            TPool pool = Build();

            PoolItem<T> item = pool.Get();
            item.Dispose();

            Assert.Null((T)item);
        }
    }
}
