using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class IPool_Generic_Test<TPool, T>
    {
        [Fact]
        public void Get_Success()
        {
            TPool pool = Build();

            Assert.NotNull(pool.Get().Item);
        }

		[Fact]
		public void Get_Multiple_IsDifferent()
        {
            TPool pool = Build();

            PoolItem<T> itemOne = pool.Get();
            PoolItem<T> itemTwo = pool.Get();

            Assert.NotEqual(itemOne, itemTwo);
            Assert.False(itemOne.Item.Equals(itemTwo.Item));
        }
    }
}
