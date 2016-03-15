using System.Collections.Generic;

using Atomos.Atomos;

using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class Pool_Generic_Test_Flexible<T>
    {
        [Theory]
        [InlineData(0, 10, 0)]
        [InlineData(10, 10, 1)]
        [InlineData(100, 10, 10)]
        [InlineData(1000, 10, 100)]
        public void Reset_NoDestroy(int itemCount, int step, int count)
        {
            List<T> items = new List<T>();
            for (int i = 0; i < itemCount; i++)
            {
                T item = _pool.Get();
                if ((i % step) == 0)
                    items.Add(item);
            }

            _pool.Set(items);
            _pool.Reset();

            Assert.Equal(count, _pool.Count);
        }
    }
}
