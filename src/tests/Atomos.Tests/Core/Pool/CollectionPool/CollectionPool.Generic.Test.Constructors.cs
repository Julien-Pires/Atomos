using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class CollectionPool_Generic_Test<TPool, TItem>
    {
        [Theory]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void Ctor_WhenInvalidCollectionPoolMode_ThrowException(int mode)
        {
            Assert.Throws<PoolException>(() => Build((CollectionPoolMode)mode));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void Ctor_WhenNegativeInitialCapacity_ThrowException(int initialCapacity)
        {
            Assert.Throws<PoolException>(() => Build(initialCapacity: initialCapacity));
        }
    }
}