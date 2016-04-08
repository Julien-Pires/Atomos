using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class BasePool_Generic_Test<TPool, TItem, TParam>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1000)]
        public void Reset_WhenStrictMode_WithPoolContainsAllCreatedElement(int itemCount)
        {
            TPool pool = Build();

            for (int i = 0; i < itemCount; i++)
                pool.Get();

            pool.Reset();

            Assert.Equal(itemCount, pool.Count);
        }
    }
}