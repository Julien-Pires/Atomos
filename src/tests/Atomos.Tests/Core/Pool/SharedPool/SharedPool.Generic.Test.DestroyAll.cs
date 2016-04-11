using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class SharedPool_Generic_Test<T>
    {
        [Theory]
        [InlineData("Foo", 10)]
        [InlineData("FooBar", 1000)]
        public void DestroyAll_Success(string name, int itemCount)
        {
            Pool<T> pool = SharedPool<T>.Get(name);
            for (int i = 0; i < itemCount; i++)
                pool.Get();

            pool.Reset();
            SharedPool<T>.DestroyAll();

            Assert.Equal(0, SharedPool<T>.Get(name).Count);
        }
    }
}
