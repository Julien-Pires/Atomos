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

        #endregion
    }
}
