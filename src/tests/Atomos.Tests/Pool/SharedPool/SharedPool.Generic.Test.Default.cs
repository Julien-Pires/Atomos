using Atomos;

using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class SharedPool_Generic_Test<T>
    {
        [Fact]
        public void Default_NotNull()
        {
            Assert.NotNull(SharedPool<T>.Default);
        }
    }
}
