using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class CollectionPool_Generic_Test_Any<TPool, TItem>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1000)]
        public void Get_WhenNoParameter_WithSucess(int initialCapacity)
        {
            CollectionPool<TItem> pool = Builder.WithInitialCapacity(initialCapacity);
            TItem item = pool.Get();

            Assert.NotNull(item);
            Assert.Equal(initialCapacity, GetCapacity(item));
        }

        [Theory]
        [InlineData(0, 10)]
        [InlineData(1000, 15)]
        public void Get_WhenParameter_WithSuccess(int initialCapacity, int capacity)
        {
            CollectionPool<TItem> pool = Builder.WithInitialCapacity(initialCapacity);
            TItem item = pool.Get(capacity);

            Assert.NotNull(item);
            Assert.Equal(initialCapacity, GetCapacity(item));
            Assert.NotEqual(capacity, GetCapacity(item));
        }
    }
}