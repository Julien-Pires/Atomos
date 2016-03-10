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
        public void Constructor_Initial_Capacity(int capacity, int count)
        {
            Pool<T> pool = new Pool<T>(capacity);

            Assert.Equal(count, pool.Count);
        }

		[Fact]
		public void Constructor_Default_Initializer()
        {
            Pool<T> pool = new Pool<T>();

            Assert.Equal(0, pool.Get().Value);
        }

		[Theory]
        [InlineData(10)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void Constructor_Custom_Initializer(int value)
        {
            Pool<T> pool = new Pool<T>(() => new T() { Value = value }, c => { });

            Assert.Equal(value, pool.Get().Value);
        }

		[Fact]
		public void Constructor_Default_Reset()
        {
            Pool<T> pool = new Pool<T>();
            T item = pool.Get();
            pool.Set(item);

            Assert.Equal(0, pool.Get().Value);
        }
    }
}