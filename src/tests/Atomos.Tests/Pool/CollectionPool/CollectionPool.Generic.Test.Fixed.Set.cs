using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class CollectionPool_Generic_Test_Fixed<TPool, TItem>
    {
        [Theory]
        [InlineData(15)]
        public void Set_WhenIdenticalCapacity_WithSuccess(int capacity)
        {
            CollectionPool<TItem> pool = Builder.WithInitialCapacity(capacity);

            pool.Set(pool.Get());
        }

        [Theory]
        [InlineData(15, 5)]
        public void Set_WhenNotIdenticalCapacity_WithFailure(int initialCapacity, int otherCapacity)
        {
            CollectionPool<TItem> pool = Builder.WithInitialCapacity(initialCapacity)
                                                .WithPoolingMode(PoolingMode.Flexible);
            TItem item = CreateItem(otherCapacity);

            Assert.Throws<PoolException>(() => pool.Set(item));
        }
    }
}