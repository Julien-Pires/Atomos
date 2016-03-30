using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class CollectionPool_Generic_Test_Fixed<TPool, TItem>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(100)]
        public void Get_WhenNoParameter_WithInitialCapacity(int initialCapacity)
        {
            CollectionPool<TItem> pool = Builder.WithInitialCapacity(initialCapacity);
            TItem item = pool.Get();

            Assert.Equal(initialCapacity, GetCapacity(item));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(100)]
        public void Get_WhenParameter_WithInitialCapacity(int initialCapacity)
        {
            CollectionPool<TItem> pool = Builder.WithInitialCapacity(initialCapacity);
            TItem item = pool.Get(initialCapacity);

            Assert.Equal(initialCapacity, GetCapacity(item));
        }

        [Theory]
        [InlineData(0, 111)]
        [InlineData(10, 222)]
        [InlineData(100, 333)]
        public void Get_WhenParameterIsDifferent_WithInitialCapacity(int initialCapacity, int parameter)
        {
            CollectionPool<TItem> pool = Builder.WithInitialCapacity(initialCapacity);
            TItem item = pool.Get(parameter);

            Assert.Equal(initialCapacity, GetCapacity(item));
        }
    }
}