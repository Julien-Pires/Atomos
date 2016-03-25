using System;
using System.Linq;
using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class SharedPool_Generic_Test<T>
    {
        [Theory]
        [InlineData("Foo")]
        [InlineData("FooBar")]
        [InlineData("$Mega_Foo$")]
        [InlineData("$$**!!")]
        public void Get_WithNoParameter_Success(string name)
        {
            Assert.NotNull(SharedPool<T>.Get(name));
        }

        [Theory]
        [InlineData("Foo", 0)]
        [InlineData("FooBar", 7777)]
        [InlineData("$Mega_Foo$", 50000)]
        public void Get_WithParameter_Success(string name, int capacity)
        {
            PoolSettings<T> settings = new PoolSettings<T> { Capacity = capacity };
            Pool<T> pool = SharedPool<T>.Get(name, settings);

            Assert.NotNull(pool);
            Assert.Equal(capacity, pool.Count);
        }

        [Theory]
        [InlineData("Foo", 3)]
        [InlineData("Foo", 100)]
        public void Get_Multiple_IsIdentical(string name, int iteration)
        {
            Pool<T>[] pools = new Pool<T>[iteration];
            for (int i = 0; i < iteration; i++)
                pools[i] = SharedPool<T>.Get(name);

            Assert.Equal(1, pools.Distinct().Count());
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("       ")]
        public void Get_WithEmptyName_ThrowException(string name)
        {
            Assert.Throws<ArgumentException>(() => SharedPool<T>.Get(name));
        }

        [Fact]
        public void Get_WithNullName_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => SharedPool<T>.Get(null));
        }
    }
}
