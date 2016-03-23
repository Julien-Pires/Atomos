using Atomos.Atomos;

using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class IPool_Generic_Test<TPool, T>
    {
        [Fact]
        public void Get_Success()
        {
            Assert.NotNull(Pool.Get().Item);
        }

		[Fact]
		public void Get_Multiple_IsDifferent()
        {
            PoolItem<T> itemOne = Pool.Get();
            PoolItem<T> itemTwo = Pool.Get();

            Assert.NotEqual(itemOne, itemTwo);
            Assert.NotEqual(itemOne.Item, itemTwo.Item);
        }
    }
}
