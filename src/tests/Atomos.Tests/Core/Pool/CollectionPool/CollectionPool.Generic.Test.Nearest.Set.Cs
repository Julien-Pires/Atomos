using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class CollectionPool_Generic_Test<TPool, TItem>
    {
        [Theory]
        [InlineData(100)]
        [InlineData(500)]
        public void Set_WhenNearestModeAndAnyCapacity_WithSuccess(int capacity)
        {
            TPool pool = Build(CollectionPoolMode.Nearest);
            pool.Set(pool.Get(capacity));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(7777)]
        public void Set_WhenNearestModeAndFlexibleAndAnyCapacity_WithSuccess(int capacity)
        {
            TPool pool = Build(CollectionPoolMode.Nearest, mode: PoolingMode.Flexible);
            TItem item = CreateItem(capacity);

            pool.Set(item);
        }
    }
}