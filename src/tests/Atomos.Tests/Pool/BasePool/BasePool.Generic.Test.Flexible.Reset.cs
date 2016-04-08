using System.Collections.Generic;

using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class BasePool_Generic_Test<TPool, TItem, TParam>
    {
        [Theory]
        [InlineData(0, 10, 0)]
        [InlineData(100, 10, 10)]
        public void Reset_WhenFlexibleMode_WithPoolContainsOnlyReturnedElement(int itemCount, int step, int count)
        {
            TPool pool = Build(mode: PoolingMode.Flexible);

            List<TItem> items = new List<TItem>();
            for (int i = 0; i < itemCount; i++)
            {
                TItem item = pool.Get();
                if ((i % step) == 0)
                    items.Add(item);
            }

            pool.Set(items);
            pool.Reset();

            Assert.Equal(count, pool.Count);
        }
    }
}
