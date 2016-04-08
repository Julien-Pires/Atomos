using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class CollectionPool_Generic_Test<TPool, TItem>
    {
        [Fact]
        public void Set_WhenAnyModeAndAnyItem_WithSuccess()
        {
            TPool pool = Build();
            pool.Set(pool.Get());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5000)]
        public void Set_WhenAnyModeAndFlexibleAndAnyItem_WithSuccess(int capacity)
        {
            TPool pool = Build(mode: PoolingMode.Flexible);
            TItem item = CreateItem(capacity);
            pool.Set(item);
        }
    }
}