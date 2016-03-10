using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class Pool_Generic_Test<T>
    {
        [Fact]
        public void Get_Success()
        {
            Assert.NotNull(_pool.Get());
        }

		[Fact]
		public void Get_Multiple_Is_Different()
        {
            T itemOne = _pool.Get();
            T itemTwo = _pool.Get();

            Assert.NotEqual(itemOne, itemTwo);
        }
    }
}
