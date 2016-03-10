using System.Collections.Generic;

using Atomos.Atomos;
using Xunit;

namespace Atomos.Tests
{
    public class PoolTest
    {
        #region Nested

        private class PoolItemTest
        {
            #region Properties

            public int Value { get; set; }

            #endregion
        }

        #endregion

        #region Fields

        private Pool<PoolItemTest> _pool;

        #endregion

        #region Constructors

        public PoolTest()
        {
            _pool = new Pool<PoolItemTest>();
        }

        #endregion

        #region Pooling Tests

        [Fact]
        public void Pool_Get_Success()
        {
            Assert.NotNull(_pool.Get());
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 1)]
        [InlineData(100, 1)]
        [InlineData(50000, 1)]
        public void Pool_Set_Success(int items, int count)
        {
            for (int i = 0; i < items; i++)
            {
                PoolItemTest item = _pool.Get();
                _pool.Set(item);
            }

            Assert.Equal(count, _pool.Count);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 10)]
        [InlineData(100, 100)]
        [InlineData(50000, 50000)]
        [InlineData(500000, 500000)]
        public void Pool_Set_Available(int items, int count)
        {
            List<PoolItemTest> itemsList = new List<PoolItemTest>(items);
            for (int i = 0; i < items; i++)
                itemsList.Add(_pool.Get());

            _pool.Set(itemsList);

            Assert.Equal(count, _pool.Count);
        }

        #endregion
    }
}
