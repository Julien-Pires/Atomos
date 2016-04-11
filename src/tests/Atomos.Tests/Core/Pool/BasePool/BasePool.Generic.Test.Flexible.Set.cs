using System.Linq;
using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class BasePool_Generic_Test<TPool, TItem, TParam>
    {
		[Fact]
        public void Set_WhenFlexibleModeAndUnknownSingleItem_WithSuccess()
        {
            TPool pool = Build(mode: PoolingMode.Flexible);

            pool.Set(CreateItem(0));

            Assert.Equal(1, pool.Count);
            Assert.NotNull(pool.Get());
        }

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        public void Set_WhenFlexibleModeAndUnknownMultipleItems_WithSuccess(int count)
        {
            TPool pool = Build(mode: PoolingMode.Flexible);

            pool.Set(Enumerable.Range(0, count).Select(c => CreateItem(0)));

            Assert.Equal(count, pool.Count);
            Assert.NotNull(pool.Get());
        }
    }
}