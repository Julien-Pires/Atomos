using System;

using Atomos.Atomos;

using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class SharedPool_Generic_Test<T>
    {
        [Theory]
        [InlineData("Foo", 10)]
        [InlineData("FooBar", 1000)]
        public void Destroy_Existing_Success(string name, int itemCount)
        {
            Pool<T> pool = SharedPool<T>.Get(name);
            for (int i = 0; i < itemCount; i++)
                pool.Get();
            pool.Reset();

            Assert.Equal(true, SharedPool<T>.Destroy(name));
            Assert.Equal(0, SharedPool<T>.Get(name).Count);
        }

        [Theory]
        [InlineData("Foo", "Bar")]
        [InlineData("Bar", "Foo")]
        public void Destroy_NoExisting_Failed(string name, string otherName)
        {
            SharedPool<T>.Get(name);

            Assert.Equal(false, SharedPool<T>.Destroy(otherName));
        }

        [Fact]
        public void Destroy_WithNullParameter_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => SharedPool<T>.Destroy(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Destroy_WithEmptyParameter_ThrowException(string name)
        {
            Assert.Throws<ArgumentException>(() => SharedPool<T>.Destroy(name));
        }
    }
}
