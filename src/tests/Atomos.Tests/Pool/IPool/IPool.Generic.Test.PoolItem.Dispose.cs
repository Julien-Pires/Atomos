using System;

using Atomos;

using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class IPool_Generic_Test<TPool, T>
    {
        [Fact]
        public void Dispose_Item_IsAvailable()
        {
            using (PoolItem<T> item = Pool.Get())
            {
            }

            Assert.Equal(1, Pool.Count);
        }

        [Fact]
        public void Dipose_Item_Twice_DoesNotThrowException()
        {
            PoolItem<T> item = Pool.Get();
            item.Dispose();
            item.Dispose();
        }

        [Fact]
        public void Dispose_ReleasedItem_ThrowException()
        {
            PoolItem<T> item = Pool.Get();
            Pool.Set(item);

            Assert.Throws<ArgumentException>(() => item.Dispose());
        }
    }
}
