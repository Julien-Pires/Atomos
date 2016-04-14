using System;
using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class CollectionPool_Generic_Test<TPool, TItem>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(7777)]
        public void Ctor_WhenCustomCollectionCapacity_WithCollectionHaveCorrectSize(int capacity)
        {
            TPool pool = Build(initialCapacity: capacity);

            Assert.Equal(capacity, GetCapacity(pool.Get()));
        }

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
            Assert.Throws<ArgumentOutOfRangeException>(() => Build(initialCapacity: initialCapacity));
        }
    }
}