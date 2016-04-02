using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class CollectionPool_Generic_Test<TPool, TItem>
    {
        [Fact]
        public void Get_WhenDefiniteModeAndNoParameter_WithSuccess()
        {
            TPool pool = Build(CollectionPoolMode.Definite);
            TItem item = pool.Get();

            Assert.NotNull(item);
        }

        [Theory]
        [InlineData(4, 16)]
        [InlineData(10, 500)]
        public void Get_WhenDefiniteModeAndParameter_WithSuccess(int initialCapacity, int capacity)
        {
            TPool pool = Build(CollectionPoolMode.Definite, initialCapacity);
            TItem item = pool.Get(capacity);

            Assert.NotEqual(initialCapacity, GetCapacity(item));
            Assert.Equal(capacity, GetCapacity(item));
        }
    }
}