using Atomos.Atomos;

using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class Pool_Generic_Test<T>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        public void Reset_NoClean(int itemCount)
        {
            for (int i = 0; i < itemCount; i++)
                _pool.Get();

            _pool.Reset();

            Assert.Equal(itemCount, _pool.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1000)]
        [InlineData(int.MaxValue)]
        public void Reset_NoClean_WithCustomReset(int value)
        {
            PoolSettings<T> settings = new PoolSettings<T> { Reset = c => c.Value = value };
            Pool<T> pool = new Pool<T>(settings);
            _pool.Get();
            _pool.Reset();

            Assert.Equal(value, _pool.Get().Value);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 0)]
        [InlineData(100, 0)]
        [InlineData(1000, 0)]
        public void Reset_CleanItems(int itemCount, int availableItems)
        {
            for (int i = 0; i < itemCount; i++)
                _pool.Get();

            _pool.Reset(true);

            Assert.Equal(availableItems, _pool.Count);
        }
    }
}
