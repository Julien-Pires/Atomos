using System;

using Atomos.Atomos;

using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class Pool_Generic_Test<T>
    {
		[Fact]
        public void Constructor_No_Capacity()
        {
            Pool<T> pool = new Pool<T>();

            Assert.Equal(0, pool.Count);
        }

        [Theory]
		[InlineData(0, 0)]
        [InlineData(10, 10)]
        [InlineData(100, 100)]
        public void Constructor_InitialCapacity(int capacity, int count)
        {
            PoolSettings<T> settings = new PoolSettings<T> { Capacity = capacity };
            Pool <T> pool = new Pool<T>(settings);

            Assert.Equal(count, pool.Count);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void Constructor_NegativeCapacity_ThrowException(int capacity)
        {
            PoolSettings<T> settings = new PoolSettings<T> { Capacity = capacity };

            Assert.Throws<ArgumentOutOfRangeException>(() => new Pool<T>(settings));
        }

		[Fact]
		public void Constructor_DefaultInitializer()
        {
            Pool<T> pool = new Pool<T>();

            Assert.Equal(0, pool.Get().Value);
        }

		[Theory]
        [InlineData(10)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void Constructor_CustomInitializer(int value)
        {
            PoolSettings<T> settings = new PoolSettings<T> { Initializer = () => new T() { Value = value } };
            Pool<T> pool = new Pool<T>(settings);

            Assert.Equal(value, pool.Get().Value);
        }

		[Fact]
		public void Constructor_DefaultReset()
        {
            Pool<T> pool = new Pool<T>();
            T item = pool.Get();
            pool.Set(item);

            Assert.Equal(0, pool.Get().Value);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(7777)]
        [InlineData(9999)]
        [InlineData(int.MaxValue)]
        public void Constructor_CustomReset(int value)
        {
            PoolSettings<T> settings = new PoolSettings<T> { Reset = c => c.Value = value };
            Pool<T> pool = new Pool<T>(settings);
            T item = pool.Get();
            pool.Set(item);

            Assert.Equal(value, pool.Get().Value);
        }
    }
}