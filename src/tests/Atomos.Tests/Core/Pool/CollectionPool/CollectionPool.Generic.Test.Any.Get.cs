using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class CollectionPool_Generic_Test<TPool, TItem>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1000)]
        public void Get_WhenAnyModeAndNoParameter_WithSucess(int initialCapacity)
        {
            TPool pool = Build(CollectionPoolMode.Any, initialCapacity);
            TItem item = pool.Get();

            Assert.NotNull(item);
            Assert.Equal(initialCapacity, GetCapacity(item));
        }

        [Theory]
        [InlineData(0, 10)]
        [InlineData(1000, 15)]
        public void Get_WhenAnyModeAndParameter_WithSuccess(int initialCapacity, int capacity)
        {
            TPool pool = Build(CollectionPoolMode.Any, initialCapacity);
            TItem item = pool.Get(capacity);

            Assert.NotNull(item);
            Assert.Equal(initialCapacity, GetCapacity(item));
            Assert.NotEqual(capacity, GetCapacity(item));
        }
    }
}