using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class CollectionPool_Generic_Test_Definite<TPool, TItem>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(16)]
        [InlineData(1024)]
        public void Set_WhenAnyCollection_WithSuccess(int capacity)
        {
            CollectionPool<TItem> pool = Builder.WithPoolingMode(PoolingMode.Flexible);
            TItem item = CreateItem(capacity);

            pool.Set(item);
        }
    }
}