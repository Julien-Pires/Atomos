using Atomos;

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
            for (int i = 0; i < itemCount; i++)
                Pool.Get();

            Pool.Reset(true);

            Assert.Equal(availableItems, Pool.Count);
        }

        [Fact]
        public void Reset_CallResetOnItems()
        {
            PoolItem<T> item = Pool.Get();
            Pool.Set(item);
            Pool.Reset();

            Assert.True(((T)item).IsReset);
        }

        [Theory]
        [InlineData(10)]
        public void Reset_SettingResetParameter_OverridePoolItemInterface(int value)
        {
            Pool<T> pool = new Pool<T>(new PoolSettings<T> { Reset = c => c.Value = value });
            PoolItem<T> item = pool.Get();
            pool.Set(item);
            pool.Reset();

            Assert.Equal(value, ((T)item).Value);
        }

        [Fact]
        public void Reset_DisposeItems_WhenDestroyed()
        {
            PoolItem<T> item = Pool.Get();
            Pool.Set(item);
            Pool.Reset(true);

            Assert.True(((T)item).IsDisposed);
        }
    }
}