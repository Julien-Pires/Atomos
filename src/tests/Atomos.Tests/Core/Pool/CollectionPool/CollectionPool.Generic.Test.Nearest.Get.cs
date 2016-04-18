using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class CollectionPool_Generic_Test<TPool, TItem>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1000)]
        public void Get_WhenNearestModeAndNoParameter_WithInitialCapacity(int capacity)
        {
            TPool pool = Build(CollectionPoolMode.Nearest, capacity);

            TItem item = pool.Get();

            Assert.NotNull(item);
            Assert.Equal(capacity, GetCapacity(item));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(7777)]
        public void Get_WhenNearestModeAndParameterAndNoItem_WithSuccess(int capacity)
        {
            TPool pool = Build(CollectionPoolMode.Nearest);

            TItem item = pool.Get(capacity);

            Assert.NotNull(item);
            Assert.Equal(capacity, GetCapacity(item));
        }

        [Theory]
        [InlineData(100)]
        [InlineData(5000)]
        public void Get_WhenNearestModeAndParameterAndItemWithExactCapacity_WithSuccess(int capacity)
        {
            TPool pool = Build(CollectionPoolMode.Nearest, capacity, initialPoolCapacity:1);

            TItem item = pool.Get(capacity);
            pool.Set(item);

            Assert.NotNull(item);
            Assert.Equal(capacity, GetCapacity(item));
            Assert.Equal(1, pool.Count);
        }

        [Theory]
        [InlineData(200)]
        [InlineData(2000)]
        public void Get_WhenNearestModeAndParameterAndItemWithSuperiorCapacity_WithSuccess(int capacity)
        {
            TPool pool = Build(CollectionPoolMode.Nearest, capacity + 1, initialPoolCapacity: 1);

            TItem item = pool.Get(capacity);
            pool.Set(item);

            Assert.NotNull(item);
            Assert.True(GetCapacity(item) > capacity);
            Assert.Equal(1, pool.Count);
        }

        [Theory]
        [InlineData(200)]
        [InlineData(2000)]
        public void Get_WhenNearestModeAndParameterAndItemWithInferiorCapacity_WithSuccess(int capacity)
        {
            TPool pool = Build(CollectionPoolMode.Nearest, capacity - 1, initialPoolCapacity: 1);

            TItem item = pool.Get(capacity);
            pool.Set(item);

            Assert.NotNull(item);
            Assert.Equal(capacity, GetCapacity(item));
            Assert.Equal(2, pool.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1000)]
        public void Get_WhenNearestModeAndParameterAndAvailablesItems_WithSucess(int capacity)
        {
            TPool pool = Build(CollectionPoolMode.Nearest);
            pool.Set(pool.Get(capacity - 1));
            pool.Set(pool.Get(capacity + 1));

            TItem item = pool.Get(capacity);
            pool.Set(item);

            Assert.NotNull(item);
            Assert.True(GetCapacity(item) > capacity);
            Assert.Equal(2, pool.Count);
        }
    }
}