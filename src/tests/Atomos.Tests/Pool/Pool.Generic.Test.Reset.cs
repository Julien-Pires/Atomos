using Atomos.Atomos;

using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class Pool_Generic_Test<T>
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 0)]
        [InlineData(100, 0)]
        [InlineData(1000, 0)]
        public void Reset_DestroyItems(int itemCount, int availableItems)
        {
            for (int i = 0; i < itemCount; i++)
                Pool.Get();

            Pool.Reset(true);

            Assert.Equal(availableItems, Pool.Count);
        }
    }
}