using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class CollectionPool_Generic_Test<TPool, TItem>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(15)]
        public void Set_WhenFixedModeAndIdenticalCapacity_WithSuccess(int capacity)
        {
            TPool pool = Build(CollectionPoolMode.Fixed, capacity);

            pool.Set(pool.Get());
        }

        [Theory]
        [InlineData(15, 5)]
        public void Set_WhenFixedModeAndNotIdenticalCapacity_WithFailure(int initialCapacity, int otherCapacity)
        {
            TPool pool = Build(CollectionPoolMode.Fixed, initialCapacity, PoolingMode.Flexible);
            TItem item = CreateItem(otherCapacity);

            Assert.Throws<PoolException>(() => pool.Set(item));
        }
    }
}