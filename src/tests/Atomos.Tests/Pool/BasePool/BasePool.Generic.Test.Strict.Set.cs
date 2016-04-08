using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class BasePool_Generic_Test<TPool, TItem, TParam>
    {
        [Fact]
        public void Set_WhenStrictModeAndUnknownSingleItem_ThrowException()
        {
            TPool pool = Build();

            Assert.Throws<PoolException>(() => pool.Set(CreateItem(0)));
        }

        [Fact]
        public void Set_UnknownMultipleItems_ThrowException()
        {
            TPool pool = Build();

            Assert.Throws<PoolException>(() => pool.Set(new []{ CreateItem(0) }));
        }
    }
}
