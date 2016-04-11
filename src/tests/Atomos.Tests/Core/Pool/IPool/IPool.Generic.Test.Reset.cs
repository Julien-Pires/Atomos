using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class IPool_Generic_Test<TPool, T>
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 0)]
        [InlineData(100, 0)]
        [InlineData(1000, 0)]
        public void Reset_DestroyItems(int itemCount, int availableItems)
        {
            TPool pool = Build();

            for (int i = 0; i < itemCount; i++)
                pool.Get();

            pool.Reset(true);

            Assert.Equal(availableItems, pool.Count);
        }

        [Fact]
        public void Reset_CallResetOnItems()
        {
            TPool pool = Build();

            PoolItem<T> item = pool.Get();
            pool.Set(item);
            pool.Reset();

            Assert.True(IsReset(item));
        }

        [Fact]
        public void Reset_DisposeItems_WhenDestroyed()
        {
            TPool pool = Build();

            PoolItem<T> item = pool.Get();
            pool.Set(item);
            pool.Reset(true);

            Assert.True(IsDisposed(item));
        }
    }
}