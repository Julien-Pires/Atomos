using System;
using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class IPool_Generic_Test<TPool, T>
    {
        [Fact]
        public void Dispose_Success()
        {
            TPool pool = Build();

            PoolItem<T> item = pool.Get();
            pool.Set(item);
            pool.Dispose();

            Assert.True(IsDisposed(item));
        }

        [Fact]
        public void Dispose_Twice_DoesNotThrowException()
        {
            TPool pool = Build();

            pool.Dispose();
            pool.Dispose();
        }

        [Fact]
        public void Dispose_CallGet_ThrowException()
        {
            TPool pool = Build();

            pool.Dispose();

            Assert.Throws<ObjectDisposedException>(() => pool.Get());
        }

        [Fact]
        public void Dispose_CallSet_ThrowException()
        {
            TPool pool = Build();

            PoolItem<T> item = pool.Get();
            pool.Dispose();

            Assert.Throws<ObjectDisposedException>(() => pool.Set(item));
        }

        [Fact]
        public void Dispose_CallSetMultiple_ThrowException()
        {
            TPool pool = Build();

            PoolItem<T> item = pool.Get();
            pool.Dispose();

            Assert.Throws<ObjectDisposedException>(() => pool.Set(new []{ (T)item }));
        }

        [Fact]
        public void Dispose_CallReset_ThrowException()
        {
            TPool pool = Build();

            pool.Dispose();

            Assert.Throws<ObjectDisposedException>(() => pool.Reset());
        }
    }
}