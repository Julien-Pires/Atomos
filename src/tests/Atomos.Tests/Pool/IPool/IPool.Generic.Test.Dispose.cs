using System;
using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class IPool_Generic_Test<TPool, T>
    {
        [Fact]
        public void Dispose_Success()
        {
            PoolItem<T> item = Pool.Get();
            Pool.Set(item);
            Pool.Dispose();

            Assert.True(((T)item).IsDisposed);
        }

        [Fact]
        public void Dispose_Twice_DoesNotThrowException()
        {
            Pool.Dispose();
            Pool.Dispose();
        }

        [Fact]
        public void Dispose_CallGet_ThrowException()
        {
            Pool.Dispose();

            Assert.Throws<ObjectDisposedException>(() => Pool.Get());
        }

        [Fact]
        public void Dispose_CallSet_ThrowException()
        {
            PoolItem<T> item = Pool.Get();
            Pool.Dispose();

            Assert.Throws<ObjectDisposedException>(() => Pool.Set(item));
        }

        [Fact]
        public void Dispose_CallSetMultiple_ThrowException()
        {
            PoolItem<T> item = Pool.Get();
            Pool.Dispose();

            Assert.Throws<ObjectDisposedException>(() => Pool.Set(new []{ (T)item }));
        }

        [Fact]
        public void Dispose_CallReset_ThrowException()
        {
            Pool.Dispose();

            Assert.Throws<ObjectDisposedException>(() => Pool.Reset());
        }
    }
}