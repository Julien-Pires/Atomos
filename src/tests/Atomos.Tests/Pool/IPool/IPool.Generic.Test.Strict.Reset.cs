using Atomos;

using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class IPool_Generic_Test_Strict<TPool, T>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        public void Reset_NoDestroy(int itemCount)
        {
            for (int i = 0; i < itemCount; i++)
                Pool.Get();

            Pool.Reset();

            Assert.Equal(itemCount, Pool.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1000)]
        [InlineData(int.MaxValue)]
        public void Reset_NoDestroy_WithCustomReset(int value)
        {
            PoolSettings<T> settings = new PoolSettings<T> { Reset = c => c.Value = value };
            Pool<T> pool = new Pool<T>(settings);
            pool.Get();
            pool.Reset();

            Assert.Equal(value, ((T)pool.Get()).Value);
        }
    }
}