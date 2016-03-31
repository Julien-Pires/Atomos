using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class CollectionPool_Generic_Test_Definite<TPool, TItem>
    {
        public void Get_WhenNoParameter_WithSuccess()
        {
            CollectionPool<TItem> pool = Builder;
            TItem item = pool.Get();

            Assert.NotNull(item);
        }

        [Theory]
        [InlineData(4, 16)]
        [InlineData(10, 500)]
        public void Get_WhenParameter_WithSuccess(int initialCapacity, int capacity)
        {
            CollectionPool<TItem> pool = Builder.WithInitialCapacity(initialCapacity);
            TItem item = pool.Get(capacity);

            Assert.NotEqual(initialCapacity, GetCapacity(item));
            Assert.Equal(capacity, GetCapacity(item));
        }
    }
}