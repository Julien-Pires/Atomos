using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class IPool_Generic_Test<TPool, T>
    {
        [Fact]
        public void Dispose_Item_IsAvailable()
        {
            TPool pool = Build();

            using (PoolItem<T> item = pool.Get())
            {
            }

            Assert.Equal(1, pool.Count);
        }

        [Fact]
        public void Dipose_Item_Twice_DoesNotThrowException()
        {
            TPool pool = Build();

            PoolItem<T> item = pool.Get();
            item.Dispose();
            item.Dispose();
        }

        [Fact]
        public void Dispose_ReleasedItem_ThrowException()
        {
            TPool pool = Build();

            PoolItem<T> item = pool.Get();
            pool.Set(item);

            Assert.Throws<PoolException>(() => item.Dispose());
        }
    }
}
