using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class CollectionPool_Generic_Test<TPool, TItem>
    {
        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        public void Set_WhenDefiniteModeAndAnyCapacity_WithSuccess(int capacity)
        {
            TPool pool = Build(CollectionPoolMode.Definite);
            pool.Set(pool.Get(capacity));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(16)]
        public void Set_WhenDefiniteModeAndFlexibleAndAnyCapacity_WithSuccess(int capacity)
        {
            TPool pool = Build(CollectionPoolMode.Definite, mode: PoolingMode.Flexible);
            TItem item = CreateItem(capacity);

            pool.Set(item);
        }
    }
}