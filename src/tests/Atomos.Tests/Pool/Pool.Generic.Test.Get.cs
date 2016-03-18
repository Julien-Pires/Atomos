using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class Pool_Generic_Test<T>
    {
        [Fact]
        public void Get_Success()
        {
            Assert.NotNull(Pool.Get());
        }

		[Fact]
		public void Get_Multiple_IsDifferent()
        {
            T itemOne = Pool.Get();
            T itemTwo = Pool.Get();

            Assert.NotEqual(itemOne, itemTwo);
        }
    }
}
