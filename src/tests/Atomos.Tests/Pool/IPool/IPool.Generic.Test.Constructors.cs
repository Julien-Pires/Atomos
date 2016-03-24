using System;

using Atomos;

using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class IPool_Generic_Test<TPool, T>
    {
		[Fact]
        public void Constructor_No_Capacity()
        {
            PoolSettings<T> settings = new PoolSettings<T> { Mode = Mode };
            Pool<T> pool = new Pool<T>(settings);

            Assert.Equal(0, pool.Count);
        }

        [Theory]
		[InlineData(0)]
        [InlineData(10)]
        [InlineData(100)]
        public void Constructor_InitialCapacity(int capacity)
        {
            PoolSettings<T> settings = new PoolSettings<T> { Capacity = capacity, Mode = Mode };
            Pool <T> pool = new Pool<T>(settings);

            Assert.Equal(capacity, pool.Count);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void Constructor_NegativeCapacity_ThrowException(int capacity)
        {
            PoolSettings<T> settings = new PoolSettings<T> { Capacity = capacity, Mode = Mode };

            Assert.Throws<ArgumentOutOfRangeException>(() => new Pool<T>(settings));
        }

		[Fact]
		public void Constructor_DefaultInitializer()
        {
            PoolSettings<T> settings = new PoolSettings<T> { Mode = Mode };
            Pool<T> pool = new Pool<T>(settings);

            Assert.Equal(0, ((T)pool.Get()).Value);
        }

		[Theory]
        [InlineData(10)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void Constructor_CustomInitializer(int value)
        {
            PoolSettings<T> settings = new PoolSettings<T> { Initializer = () => new T() { Value = value }, Mode = Mode };
            Pool<T> pool = new Pool<T>(settings);

            Assert.Equal(value, ((T)pool.Get()).Value);
        }

		[Fact]
		public void Constructor_DefaultReset()
        {
            PoolSettings<T> settings = new PoolSettings<T> { Mode = Mode };
            Pool<T> pool = new Pool<T>(settings);
            T item = pool.Get();
            pool.Set(item);

            Assert.Equal(0, ((T)pool.Get()).Value);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(7777)]
        [InlineData(9999)]
        [InlineData(int.MaxValue)]
        public void Constructor_CustomReset(int value)
        {
            PoolSettings<T> settings = new PoolSettings<T> { Reset = c => c.Value = value, Mode = Mode };
            Pool<T> pool = new Pool<T>(settings);
            T item = pool.Get();
            pool.Set(item);

            Assert.Equal(value, ((T)pool.Get()).Value);
        }
    }
}