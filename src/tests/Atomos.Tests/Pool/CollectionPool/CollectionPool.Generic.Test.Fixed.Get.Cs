using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class CollectionPool_Generic_Test<TPool, TItem>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(100)]
        public void Get_WhenFixedModeAndNoParameter_WithInitialCapacity(int initialCapacity)
        {
            TPool pool = Build(CollectionPoolMode.Fixed, initialCapacity);
            TItem item = pool.Get();

            Assert.NotNull(item);
            Assert.Equal(initialCapacity, GetCapacity(item));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(100)]
        public void Get_WhenFixedModeAndParameter_WithInitialCapacity(int initialCapacity)
        {
            TPool pool = Build(CollectionPoolMode.Fixed, initialCapacity);
            TItem item = pool.Get(initialCapacity);

            Assert.NotNull(item);
            Assert.Equal(initialCapacity, GetCapacity(item));
        }

        [Theory]
        [InlineData(0, 111)]
        [InlineData(10, 222)]
        [InlineData(100, 333)]
        public void Get_WhenFixedModeAndParameterIsDifferent_WithInitialCapacity(int initialCapacity, int parameter)
        {
            TPool pool = Build(CollectionPoolMode.Fixed, initialCapacity);
            TItem item = pool.Get(parameter);

            Assert.NotNull(item);
            Assert.Equal(initialCapacity, GetCapacity(item));
        }
    }
}