using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class CollectionPool_Generic_Test<TPool, TItem>
    {
        [Theory]
        [InlineData(4)]
        [InlineData(10)]
        public void Get_WhenDefiniteModeAndNoParameter_WithSuccess(int initialCapacity)
        {
            TPool pool = Build(CollectionPoolMode.Definite, initialCapacity);
            TItem item = pool.Get();

            Assert.NotNull(item);
            Assert.Equal(initialCapacity, GetCapacity(item));
        }

        [Theory]
        [InlineData(4, 16)]
        [InlineData(10, 500)]
        public void Get_WhenDefiniteModeAndParameter_WithSuccess(int initialCapacity, int capacity)
        {
            TPool pool = Build(CollectionPoolMode.Definite, initialCapacity);
            TItem item = pool.Get(capacity);

            Assert.NotNull(item);
            Assert.NotEqual(initialCapacity, GetCapacity(item));
            Assert.Equal(capacity, GetCapacity(item));
        }
    }
}