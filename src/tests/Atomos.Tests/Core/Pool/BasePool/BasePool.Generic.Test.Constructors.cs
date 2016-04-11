using System;
using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class BasePool_Generic_Test<TPool, TItem, TParam>
    {
		[Fact]
        public void Constructor_No_Capacity()
        {
            TPool pool = Build();

            Assert.Equal(0, pool.Count);
        }

        [Theory]
		[InlineData(0)]
        [InlineData(10)]
        public void Constructor_InitialCapacity(int capacity)
        {
            TPool pool = Build(capacity);

            Assert.Equal(capacity, pool.Count);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void Constructor_NegativeCapacity_ThrowException(int capacity)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Build(capacity));
        }

		[Fact]
		public void Constructor_DefaultInitializer()
        {
            TPool pool = Build();

            Assert.Equal(0, GetValue(pool.Get()));
        }

		[Theory]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void Constructor_CustomInitializer(int value)
		{
		    TPool pool = Build(initializer: () => CreateItem(value));

            Assert.Equal(value, GetValue(pool.Get()));
        }

        [Theory]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void Constructor_DefaultReset(int value)
        {
            TPool pool = Build(initializer: () => CreateItem(value));

            TItem item = pool.Get();
            pool.Set(item);

            Assert.Equal(value, GetValue(pool.Get()));
        }

        [Theory]
        [InlineData(10)]
        [InlineData(7777)]
        public void Constructor_CustomReset(int value)
        {
            TPool pool = Build(initializer: () => CreateItem(value), reset: ResetItem);

            TItem item = pool.Get();
            pool.Set(item);

            Assert.Equal(0, GetValue(pool.Get()));
        }

        [Theory]
        [InlineData(10)]
        public void Constructor_SettingResetParameter_OverridePoolItemInterface(int value)
        {
            TPool pool = Build(initializer: () => CreateItem(value), reset: ResetItem);

            PoolItem<TItem> item = pool.Get();
            pool.Set(item);
            pool.Reset();

            Assert.NotEqual(value, GetValue(item));
            Assert.Equal(0, GetValue(item));
        }
    }
}